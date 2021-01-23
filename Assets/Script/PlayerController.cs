using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SpriteShatter; <- paid asset
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int playerId, controllerId;
    public float speed = 100, jumpVelocity = 150, lowJumpMult = 2f, fallMult = 2.5f;

    private Vector2 input, shoot_arm;

    private Rigidbody2D rig, arm;


    //Jump settings
    [Header("Jump settings")]
    [Range(3, 30)]
    public int lines = 15;
    private bool jumpRequest, shootRequest;
    private Vector3 size;
    public LayerMask mask;
    public float dist = 0.05f;
    private bool doubleJump = true, canJump;

    [Header("Weapon settings")]
    public Weapon weapon;
    public GameObject arm_obj;
    private GameObject gunObj;
    private SpriteRenderer gunRendere;
    private Transform[] shootposes;
    private int bullets;
    private Animator gunAnim;

    [Header("Splat settings")]
    public ParticleSystem blood;
    private Color color;


    [Space]
    public bool GizmosDebug = true;


    private bool destroyed = false;
    //private Shatter shatter; <- paid asset
    private SpriteRenderer rendere;
    private Animator anim;

    private int lastDir;

    public void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        arm = arm_obj.GetComponent<Rigidbody2D>();
        size = GetComponent<BoxCollider2D>().bounds.size;
        StartCoroutine(Shoot());
        //shatter = GetComponent<Shatter>(); <- paid asset
        rendere = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">placement in stats</param>
    /// <param name="c"></param>
    public void Init(int id, Color c)
    {
        playerId = id;
        controllerId = App.Instance.playerStats[id].controllerId;
        name = "Player " + playerId;
        App.Instance.playerStats[id].health = App.Instance.playerStats[id].StartHealth();
        GetComponent<SpriteRenderer>().color = c;

        foreach (SpriteRenderer rendere in GetComponentsInChildren<SpriteRenderer>())
        {
            rendere.color = color = c;
        }

        var main = blood.main;
        main.startColor = GetComponentInChildren<BloodSplat>().splatterColor =  color;

    }

    public void PickupWeapon(Weapon w)
    {
        weapon = w;
        gunObj = weapon.InitWeapon(arm_obj.transform);

        Animator _anim = gunObj.GetComponent<Animator>();

        if (_anim)
            gunAnim = _anim;

        bullets = weapon.maxAmmo;
        gunRendere = gunObj.GetComponent<SpriteRenderer>();
        shootposes = weapon.GetShootpos();
    }

    public void Update()
    {
        if (!App.Instance.playable || destroyed)
            return;

        input = new Vector2(Input.GetAxis("move_controller_" + controllerId), Input.GetAxisRaw("move_controller_vertical_" + controllerId));
        
        if(input.x != 0)
            lastDir = (input.x > 0) ? 1 : -1;
        
        shoot_arm = new Vector2(Input.GetAxis("shoot_arm_x_" + controllerId), Input.GetAxis("shoot_arm_y_" + controllerId));

        anim.SetBool("moving", input.x != 0);


        if(shoot_arm.magnitude != 0)
        {
            float heading = Mathf.Atan2(shoot_arm.x, shoot_arm.y);
            arm.rotation = (heading * Mathf.Rad2Deg - 90);

            if(gunRendere != null)
                gunRendere.flipY = (shoot_arm.x < 0);
        }

        rendere.flipX = (shoot_arm.x < 0 || lastDir == -1);




        jumpRequest = Input.GetButtonDown("jump_" + controllerId);
        canJump = CanJump();

        if (weapon != null)
            shootRequest = (weapon.autoFire) ? Input.GetButton("shoot_" + controllerId) : Input.GetButtonDown("shoot_" + controllerId);
        else
            shootRequest = false;

        if(Input.GetButtonDown("drop_gun_" + controllerId) && gunObj != null)
        {
            ThrowGun();
        }


    }

    public void FixedUpdate()
    {
        if (!App.Instance.playable || destroyed)
            return;

        rig.position += new Vector2(input.x,0) * (speed + App.Instance.playerStats[playerId].SpeedBoost()) * Time.deltaTime;


        if (rig.velocity.y < 0)
        {
            rig.gravityScale = (fallMult - App.Instance.playerStats[playerId].Gravity());
        }
        else if(rig.velocity.y > 0 && !Input.GetButton("jump_" + controllerId))
        {

            rig.gravityScale = (lowJumpMult - App.Instance.playerStats[playerId].Gravity());
        }
        else
        {
            rig.gravityScale = (1 - App.Instance.playerStats[playerId].Gravity());
        }


        if (jumpRequest)
        {
            if (canJump)
            {
                jumpRequest = false;
                rig.AddForce(Vector2.up * jumpVelocity);
                anim.SetTrigger("jump");
            }
        }


        rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x,-speed,speed), Mathf.Clamp(rig.velocity.y,-jumpVelocity * fallMult,jumpVelocity));
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            if(App.Instance.isPlaying && App.Instance.playerStats[playerId].health > 0 && App.Instance.playable)
            {
                if (shootRequest)
                {
                    if(bullets <= 0)
                    {
                        ThrowGun();
                        yield return null;
                    }
                    else
                    {
                        Debug.Log(gameObject.name);
                        foreach (Transform sp in shootposes)
                        {
                            GameObject g = Instantiate(weapon.bullet, sp.position, Quaternion.identity);
                            g.transform.rotation = sp.rotation;
                            if (SceneManager.GetActiveScene().buildIndex != 3)
                                g.GetComponent<Bullet>().damage = weapon.damage + App.Instance.playerStats[playerId].PowerBoost();

                            g.GetComponent<Bullet>().senderName = gameObject.name;
                            g.GetComponent<Bullet>().statSender = playerId;

                            Vector2 angle = new Vector2(sp.right.x, sp.right.y);
                            rig.AddForce(jumpVelocity * weapon.recoil * (-angle));
                            arm.rotation += Random.Range(-weapon.accurency, weapon.accurency);
                        }

                        if (weapon.shell != null)
                        {
                            GameObject shell = Instantiate(weapon.shell, transform.position, Quaternion.identity);
                            shell.transform.localScale = weapon.shellScale;
                        }

                        if (gunAnim != null)
                            gunAnim.SetTrigger("shoot");

                        App.Instance.playerStats[playerId].shotsFired++;
                        App.Instance.Shake.TriggerShake(weapon.shakeTime);
                        if (weapon.muzzle != null)
                        {
                            weapon.Muzzle();
                        }



                        bullets--;
                        Juto.Audio.AudioController.PlaySound(weapon.GetRandomClip());

                        yield return new WaitForSeconds(weapon.fireRate - App.Instance.playerStats[playerId].RapidFire());
                    }
                }
            }

            yield return null;
        }
    }

    public void ThrowGun()
    {

        gunObj.AddComponent<Rigidbody2D>();
        Rigidbody2D rig = gunObj.GetComponent<Rigidbody2D>();
        rig.AddForce(arm.transform.right * 500);
        BoxCollider2D solid = gunObj.AddComponent<BoxCollider2D>();
        BoxCollider2D trigger = gunObj.AddComponent<BoxCollider2D>();

        solid.size *= 0.75f;
        trigger.size *= 1.25f;
        trigger.isTrigger = true;

        gunObj.AddComponent<ArmCollision>();

        weapon = null;
        gunObj.transform.parent = null;
        gunObj = null;
    }

    public bool CanJump()
    {
        Vector3 startPos = transform.position - new Vector3((size.x / 2 - 0.025f), (size.y / 2), 0);
        Vector3 endPos = transform.position + new Vector3((size.x / 2 - 0.025f), -(size.y / 2), 0);

        for (int i = 0; i < lines; i++)
        {
            float p = (float)i / (float)lines;

            Vector3 pos = Vector3.Lerp(startPos, endPos, p);
            Ray2D r = new Ray2D(pos, Vector2.down);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, dist, mask);

            if(hit)
            {
                doubleJump = true;
                return true;
            }
        }


        if (jumpRequest && doubleJump)
        {
            doubleJump = false;
            return true;
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        if(GizmosDebug)
        {
            if (size.magnitude == 0)
                size = GetComponent<BoxCollider2D>().bounds.size;


            Vector3 startPos = transform.position - new Vector3((size.x / 2 - 0.025f), (size.y / 2), 0);
            Vector3 endPos = transform.position + new Vector3((size.x / 2 - 0.025f), -(size.y / 2), 0);


            Gizmos.color = Color.red;
            Gizmos.DrawRay(startPos, Vector3.down);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(endPos, Vector3.down);

            Gizmos.color = Color.green;

            for (int i = 0; i < lines; i++)
            {
                float p = (float)i / (float)lines;

                Vector3 pos = Vector3.Lerp(startPos, endPos, p);

                Gizmos.DrawRay(pos, Vector3.down);
            }
        }
    }


    public void TakeDamage(float amount, Vector3 dir)
    {
        App.Instance.playerStats[playerId].health -= amount;
        App.Instance.playerStats[playerId].damageRecived += amount;
        blood.Play();
        if (App.Instance.playerStats[playerId].health <= 0)
        {
            //shatter.shatter(); <- paid asset

            if(!destroyed)
            {
                SpawnManager sp = FindObjectOfType<SpawnManager>();

                if(sp)
                {
                    sp.PlayerGotKilled(playerId,controllerId);
                }

                App.Instance.playerStats[playerId].death++;
                Destroy(gameObject, 1f);
                Destroy(arm_obj);
                destroyed = true;
            }
        }
    }
}

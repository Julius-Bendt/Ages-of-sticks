using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    [Header("Gun settings")]
    public int maxAmmo, damage, accurency; //0 = best, 360 = worst
    public float fireRate;
    [Range(0,2)]
    public float recoil;
    public bool autoFire;
    public GameObject bullet, muzzle, gun, shell;
    public float shakeTime = 0.05f;
    [Header("Positions")]
    [SerializeField]
    private Vector3 muzzlePos;
    [SerializeField]
    private Vector3 objPos, scale, worldScale;
    public Vector3 shellScale;

    private GameObject obj;
    private Transform[] shootPoses;
    private Transform parent;

    public GameObject InitWeapon(Transform _parent)
    {
        CopyValues();
        parent = _parent;
        obj = Instantiate(gun, parent);
        obj.transform.localPosition = objPos;
        obj.transform.localScale = scale;
        return obj;
    }

    public void Muzzle()
    {
        GameObject m = Instantiate(muzzle, obj.transform);
        m.transform.localPosition = muzzlePos;
        m.transform.rotation = parent.rotation;
        m.transform.localScale = new Vector3(5f, 5, 1);

        Destroy(m, 0.2f);
    }

    public Transform[] GetShootpos()
    {
        List<Transform> l = new List<Transform>();

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).name == "shootpos")
                l.Add(obj.transform.GetChild(i));
        }

        shootPoses = l.ToArray();
        return shootPoses;
    }

    [Header("Melee settings")]
    public bool melee;
    public float swingTime;
    public Vector3 swingRotate;
    public float move;
    public int durability = 999;

    public AudioClip[] fireClip;

    public AudioClip GetRandomClip()
    {
        return fireClip[Random.Range(0, fireClip.Length - 1)];
    }

    public void CopyValues()
    {
        if (gun == null)
            return;

        objPos = gun.transform.localPosition;
        scale = gun.transform.localScale;
        muzzlePos = gun.transform.Find("shootpos").localPosition;
    }

    public Vector3 GetScale() { return worldScale; }
}

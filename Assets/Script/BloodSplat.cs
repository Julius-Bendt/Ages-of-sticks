//using SplatterSystem; <- paid asset
using System.Collections.Generic;
using UnityEngine;


    public class BloodSplat : MonoBehaviour
    {
        //public AbstractSplatterManager splatter; <- paid asset
        public Color splatterColor;

        private ParticleSystem particles;

        private List<ParticleCollisionEvent> collisionEvents;

        void Start()
        {
            //splatter = FindObjectOfType<AbstractSplatterManager>(); <- paid asset
            particles = GetComponent<ParticleSystem>();

            collisionEvents = new List<ParticleCollisionEvent>(12);
        }


        void OnParticleCollision(GameObject other)
        {
            int safeLength = particles.GetSafeCollisionEventSize();

            if (collisionEvents.Count < safeLength)
            {
                collisionEvents = new List<ParticleCollisionEvent>(safeLength);
            }


            int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 position = collisionEvents[i].intersection;
                Vector3 velocity = collisionEvents[i].velocity;
                HandleParticleCollision(position, velocity);
            }
        }


        private void HandleParticleCollision(Vector3 position, Vector3 velocity)
        {
            /* paid asset
            if (splatter != null)
            {
                splatter.Spawn(position, velocity.normalized, splatterColor);
            }
            */
        }
    }


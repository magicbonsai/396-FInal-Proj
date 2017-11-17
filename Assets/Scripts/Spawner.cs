using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        public Enemy[] MobTypes; //Because we will have multiple types of enemies later so might as well keep track of them now
        public float Cooldown; //Again, leave this for now but also feel free to play with it #noinfinitespawns
        private float _lastSpawnTime;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - _lastSpawnTime >= Cooldown) //I set the Cooldown to be very low just to see if I can spawn multiple enemeis at the same time
            {
                _lastSpawnTime = Time.time;
                MobTypes[0].Initialize(5000000);
                Instantiate(MobTypes[0], gameObject.transform.position, new Quaternion(), gameObject.transform);
            }
        }
    }
}
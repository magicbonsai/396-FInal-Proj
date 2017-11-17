//New
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Tower : MonoBehaviour
    {
        public int Power; //How much damage the tower does. Be sure to set this in the inspector. Either 25 or 50 should be good.
        private bool _enemyInRange = false;
        public float Cooldown;
        private float _lastTimeFired;
        public float Range;
        public Collider[] Hit;
        private GameObject currentEnemy;
        private int closest;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {
            if (currentEnemy == null)
            {
                Hit = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Enemy")); //Stores the info of whatever was detected
                closest = ClosestEnemy(Hit);
            }


            if (closest != -1)
            {
                if (currentEnemy.CompareTag("Enemy")) //Just in case it detects a Cell by accident. If you guys can figure out how to avoid that that'd be great
                {
                    _enemyInRange = true;
                }

                if (_enemyInRange)
                {
                    gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(currentEnemy.transform.position.x - gameObject.transform.position.x, 0, currentEnemy.transform.position.z - gameObject.transform.position.z));

                    if (Time.time - _lastTimeFired >= Cooldown)
                    {
                        _lastTimeFired = Time.time;

                        GameObject bullet = (GameObject)Instantiate(Resources.Load("Bullet"), transform.GetChild(0).GetChild(0).position, new Quaternion(0, 0, 0, 0), transform);
                        bullet.GetComponent<Bullet>().Initialize(Power, currentEnemy.GetComponent<Enemy>());
                        _enemyInRange = false;
                    }
                }
            }
            //So in theory this should work? Hard to really tell without a GameObject to actually test with but it's also 4AM and Sydney is probably gonna yell at me so yeah lemme know what happens LOL
        }

        private int ClosestEnemy(Collider[] colliders)
        {
            float closestDistance = 9001.0f;
            int closestIndex = -1;

            for (int i = 0; i < colliders.Length; i++)
            {
                /*
                if (currentEnemy != null && colliders[i].gameObject.Equals(currentEnemy))
                {
                    return i;
                }
                */
                if (closestDistance > Vector3.Distance(colliders[i].transform.position, transform.position))
                {
                    closestDistance = Vector3.Distance(colliders[i].transform.position, transform.position);
                    closestIndex = i;
                }
            }

            if (closestDistance <= Range)
            {
                currentEnemy = colliders[closestIndex].gameObject;
                return closestIndex;
            }

            return -1;
        }
    }
}
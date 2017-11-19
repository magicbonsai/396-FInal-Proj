//New
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : MonoBehaviour
    {
        
        private int _power;

        public Enemy _target; //I'm using Homing Missiles becuase it'd be so dumb if a Tower misses in a Tower Defense game

        private Vector3 _targetPosition;
        private Vector3 _myPosition;
        public float Speed;

        public void Initialize(int power, Enemy target)
        {
            _power = power;
            _target = target;
            _targetPosition = _target.gameObject.transform.position;
        }

        // Use this for initialization
        private void Start()
        {
            _myPosition = gameObject.transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_target == null)
            {
                Destroy(gameObject);
            }
            _targetPosition = _target.gameObject.transform.position;
            _myPosition = gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().rotation = Quaternion.LookRotation(_targetPosition); //Looks towards target
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(_targetPosition.x - _myPosition.x, 0, _targetPosition.z - _myPosition.z); //Homes in
            gameObject.GetComponent<Rigidbody>().velocity = gameObject.GetComponent<Rigidbody>().velocity.normalized * Speed;
        }

        internal void OnCollisionEnter(Collision other)
        {
            //int gold = other.gameObject.GetComponent<Enemy>().Attack;
            if(other.gameObject.GetComponent<Enemy>())
                other.gameObject.GetComponent<Enemy>().TakeDamage(_power);
            //if (other.gameObject.GetComponent<Enemy>().enabled == false)
            //{
            //    FindObjectOfType<GameManager>().AddGold(gold);
            //    other.gameObject.GetComponent<Enemy>().Die();
            //}
            Destroy(gameObject);
            /*if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(_power);
                Destroy(gameObject);
            }*/
        }
    }
}
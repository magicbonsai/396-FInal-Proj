using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts
{
    public class Base : MonoBehaviour
    {
        //Check the inspector, it works, just trust me if you don't
        public int Health;
        public Slider HealthSlider;

        // Use this for initialization
        void Start()
        {
            HealthSlider.value = Health;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Hit(int damage)
        {
            Health -= damage;
            HealthSlider.value = Health;

            //Too lazy to figure out an actual way to make the health bar look "empty"
            if (Health <= 0)
            {
                HealthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0, 0);
                Die();
            }
        }

        void Die()
        {
            //Please I really want a death "animation" or at least an explosion
            Destroy(gameObject);
        }

        internal void OnCollisionEnter(Collision other)
        {
            //For Mike: In classic Tower Defense games, once the enemy reaches the base, the base takes damage and the enemy disappears
            Hit(other.gameObject.GetComponent<Enemy>().Attack);
            Destroy(other.gameObject);
        }

        void OnMouseUp()
        {
            Hit(10); //This is purely for testing purposes so you can see that the health bar is indeed decreasing
        }
    }
}
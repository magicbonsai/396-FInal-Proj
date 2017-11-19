using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Wave : MonoBehaviour
    {
        private float windowWidth;
        public Slider TimeRemaining;
        private int Wave_num=0;
        private float _timeUntilWave;
        private float _timeCalled;
        private float _SpawnTime;
        private float _timeOfLastSpawn = 0f;

        private int NormiesCount;
        private int FastCount;
        private int StrongCount;
        private int currentEnemy;
        private int totalEnemies;

        public int[] EnemyOrder;
        public Enemy[] MobTypes;

        //Texts
        public Text num_wave;
        public Text Normies;
        public Text Fast;
        public Text Strong;

        //include strength of enemies

        // Use this for initialization
        void Start()
        {
            PopulateWave();
        }

        // Update is called once per frame
        void Update()
        {
            //only called when the wait time is up
            if (Time.time - _timeCalled >= _timeUntilWave)
            {
                //if spawn time is reached, spawn enemies
                if (Time.time - _timeOfLastSpawn >= _SpawnTime)
                {
                    num_wave.enabled = false;
                    Normies.enabled = false;
                    Fast.enabled = false;
                    Strong.enabled = false;
                    TimeRemaining.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);

                    if (currentEnemy < totalEnemies)
                    {
                        Spawn();
                    }
                    //If we spawned all enemies and all the enemies are not on the board anymore
                    else if(GameObject.Find("Fast(Clone)")==null && GameObject.Find("Strong(Clone)")==null && GameObject.Find("Enemy(Clone)")==null)
                    {
                        Debug.Log("Repopulating!");
                        PopulateWave();
                    }
                }
            }
            //Updates slider value with the time until update
            //Updates text position as well
            else
            {
                TimeRemaining.value = _timeUntilWave-(Time.time -_timeCalled);
                Debug.Log(TimeRemaining.value);
                num_wave.GetComponent<RectTransform>().position = new Vector3(Screen.width * TimeRemaining.value / _timeUntilWave, Screen.height / 10 + 20, Normies.GetComponent<RectTransform>().position.z);
                Normies.GetComponent<RectTransform>().position = new Vector3(Screen.width*TimeRemaining.value/_timeUntilWave, num_wave.GetComponent<RectTransform>().position.y - 20, Normies.GetComponent<RectTransform>().position.z);
                Fast.GetComponent<RectTransform>().position = new Vector3(Screen.width * TimeRemaining.value / _timeUntilWave, Normies.GetComponent<RectTransform>().position.y - 20, Normies.GetComponent<RectTransform>().position.z);
                Strong.GetComponent<RectTransform>().position = new Vector3(Screen.width * TimeRemaining.value / _timeUntilWave, Fast.GetComponent<RectTransform>().position.y - 20, Normies.GetComponent<RectTransform>().position.z);
            }
        }


        void Spawn()
        {
            //What type of enemy it is
            int Enemy_type = EnemyOrder[currentEnemy];
            Instantiate(MobTypes[Enemy_type], gameObject.transform.position, new Quaternion(), gameObject.transform);
            _timeOfLastSpawn = Time.time;
            /*//Reduce associated enemy Count
            if (Enemy_type == 0)
            {
                NormiesCount--;
            }
            else if (Enemy_type == 1)
            {
                FastCount--;
            }
            else
            {
                StrongCount--;
            }*/
            //Go on to next enemy
            currentEnemy++;

        }

        void PopulateWave() {
            //Resets all fields
            Wave_num++;
            currentEnemy = 0;
            NormiesCount = 0;
            FastCount = 0;
            StrongCount = 0;
            _timeCalled = Time.time;

            //Initializing EnemyOrder
            System.Random r = new System.Random();
            //Generates the number of enemies in wave 1-20;
            EnemyOrder = new int[r.Next(1, 21)];
            for (int i = 0; i < EnemyOrder.Length; i++)
            {
                //Generates a number between 0 and 2 and populates array
                int EnemyType = r.Next(0, 3);
                EnemyOrder[i] = EnemyType;
                if (EnemyType == 0)
                {
                    NormiesCount++;
                }
                else if (EnemyType == 1)
                {
                    FastCount++;
                }
                else
                {
                    StrongCount++;
                }
            }
            //Uses number of enemies to get appropriate numbersfor spawn times and wait times
            currentEnemy = 0;
            totalEnemies = NormiesCount + FastCount + StrongCount;
            _timeUntilWave = totalEnemies * 3;
            _SpawnTime = (float)totalEnemies / 10;

            //Enable all UI elements
            num_wave.enabled = true;
            Normies.enabled = true;
            Fast.enabled = true;
            Strong.enabled = true;
            TimeRemaining.enabled = true;

            //Fix UI element position
            TimeRemaining.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height / 4);
            TimeRemaining.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height / 10, TimeRemaining.GetComponent<RectTransform>().position.z);
            TimeRemaining.maxValue = _timeUntilWave;
            TimeRemaining.value = _timeUntilWave;

            //Updates UI Texts
            num_wave.text = "Wave Number: " + Wave_num;
            num_wave.GetComponent<RectTransform>().position = new Vector3(Screen.width, Screen.height / 10 + 20, num_wave.GetComponent<RectTransform>().position.z);
            Normies.text = "Normal Enemy Count:" + NormiesCount;
            Normies.GetComponent<RectTransform>().position = new Vector3(Screen.width, num_wave.GetComponent<RectTransform>().position.y - 20, Normies.GetComponent<RectTransform>().position.z);
            Fast.text = "Fast Enemy Count:" + FastCount;
            Fast.GetComponent<RectTransform>().position = new Vector3(Screen.width, Normies.GetComponent<RectTransform>().position.y - 20, Fast.GetComponent<RectTransform>().position.z);
            Strong.text = "Strong Enemy Count:" + StrongCount;
            Strong.GetComponent<RectTransform>().position = new Vector3(Screen.width, Fast.GetComponent<RectTransform>().position.y - 20, Strong.GetComponent<RectTransform>().position.z);
        }

        void Die()
        {
            Debug.Log("Die!");
            Destroy(gameObject);
        }
    }
}
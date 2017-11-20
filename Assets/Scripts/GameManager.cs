using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public int Gold;
        public Text GoldText;
        private bool _goldActive;
        public Text WinLossText;
        public bool menuOpen;
        public GameObject Base;
        public GameObject Wave;
        //private GameObject[,] _grid;

        // Use this for initialization
        void Start()
        {
            menuOpen = false;
            _goldActive = true;
            GoldText.text = "Gold: " + Gold;
            WinLossText.text = "";
            /*_grid = new GameObject[5, 5];
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    _grid[i, j] = (GameObject)Instantiate(Resources.Load("Cell"), new Vector3(j - 2, 0, 2 - i), new Quaternion(0, 0, 0, 0), transform);
                }
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            if (Base.GetComponent<Base>().Health <= 0)
            {
                Base.GetComponent<Base>().HealthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0, 0);
                Base.GetComponent<Base>().Die();
                WinLossText.text = "As your base crumbles to the ground, all of the minions in the kingdom as well as in the portal " +
                                   "jeer at you and howl out in glee as their tireless mission comes to an end. Ashamed, you must now" +
                                   "spend the rest of eternity ruminating over your defeat, guilt having infected your body and mind." +
                                   "What was once a proud and revered leader is now a fallen ice cream cone, swarmed by ants of shame" +
                                   "and rained on by salty tears shed by their self-worth. \n Least to say... \n \n YOU LOSE";

                StopGame();
            }
        }

        public void closeMenu()
        {
            menuOpen = false;
        }

        public void AddGold(int gold)
        {
            if (_goldActive)
            {
                Gold += gold;
                GoldText.text = "Gold: " + Gold; 
            }

        }

        public void SpendGold(int gold)
        {
            if (_goldActive)
            {
                Gold -= gold;
                GoldText.text = "Gold: " + Gold;
            }
        }

        public void StopGame()
        {
            _goldActive = false;

            foreach (Cell cell in FindObjectsOfType<Cell>())
            {
                cell.enabled = false;
            }

            foreach (nonCell cell in FindObjectsOfType<nonCell>())
            {
                cell.enabled = true;
            }

            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                tower.enabled = false;
            }

            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                enemy.enabled = false;
                enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }

            Destroy(Wave);

            Destroy(Base.GetComponent<Base>());

            

            this.enabled = false;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public int Gold;
        public Text GoldText;
        //private GameObject[,] _grid;

        // Use this for initialization
        void Start()
        {
            GoldText.text = "Gold: " + Gold;
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

        }

        public void AddGold(int gold)
        {
            Gold += gold;
            GoldText.text = "Gold: " + Gold;
        }

        public void SpendGold(int gold)
        {
            Gold -= gold;
            GoldText.text = "Gold: " + Gold;
        }
    }
}
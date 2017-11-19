using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

using UnityEngine.AI;


namespace Assets.Scripts
{


    public class nonCell : MonoBehaviour
    {
        private bool _hasTower;
        // Use this for initialization
        void Start()
        {
            _hasTower = false;
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<NavMeshObstacle>().enabled = _hasTower;
        }
    }
}

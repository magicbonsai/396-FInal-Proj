using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts
{
    public class Cell : MonoBehaviour
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

        //This will be sufficient for detecting mouse clicks, in the future a menu will be added to select towers
        void OnMouseUp()
        {
            Debug.Log("Hi! My name is: (" + transform.position.x + ", " + transform.position.y + ")");
            _hasTower = true;
        }
    }
}
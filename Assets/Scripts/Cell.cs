using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Cell : MonoBehaviour
    {
        private bool _hasTower;
        private Renderer rend;
        public Color hoverColor;
        private Color startColor;
        public GameObject bMenu;
        private Vector3 cubePos;
        public GameManager gManager;
        private GameObject g;
        public GameObject fText, tText;


        // Use this for initialization
        void Start()
        {
            _hasTower = false;
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;
            cubePos = Camera.main.WorldToScreenPoint(this.transform.position);
            bMenu.transform.position = cubePos;
            bMenu.SetActive(false);
            fText.SetActive(false);
            tText.SetActive(false);
            g = GameObject.Find("Game Manager");
            gManager = g.GetComponent<GameManager>();
        }

        void OnMouseEnter()
        {
            if(!gManager.menuOpen)
                rend.material.color = hoverColor;
        }

        void OnMouseExit()
        {
            if(!gManager.menuOpen)
                rend.material.color = startColor;
        }

        // Update is called once per frame
        void Update()
        {
            GetComponent<NavMeshObstacle>().enabled = _hasTower;
            
        }
        IEnumerator Flash()
        {
            rend.material.color = Color.red; //Too tired, please pick a better color
            yield return new WaitForSeconds(0.1f);
            rend.material.color = Color.white;
        }

        //This will be sufficient for detecting mouse clicks, in the future a menu will be added to select towers
        void OnMouseUp()
        {
            if (gManager.menuOpen)
            {
                bMenu.SetActive(false);
                gManager.menuOpen = false;
                return;
            }
            //Debug.Log("Hi! My name is: (" + transform.position.x + ", " + transform.position.z + ")");
            if (!_hasTower)
            {
                GetComponent<NavMeshObstacle>().enabled = true;
                foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                {
                    if (!enemy.GetComponent<NavMeshAgent>().CalculatePath(enemy.GetComponent<NavMeshAgent>().destination, enemy.GetComponent<NavMeshAgent>().path))
                    {
                        Flash();
                        return;
                    }
                }

                bMenu.SetActive(true);
                bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.black;
                bMenu.transform.Find("upgradeButton").gameObject.GetComponent<Button>().enabled = false;
                gManager.menuOpen = true;
            }

            if (!gManager.menuOpen)
            {
                bMenu.SetActive(true);
                bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().GetComponent<Image>().color = Color.black;
                bMenu.transform.Find("buildButton").gameObject.GetComponent<Button>().enabled = false;
                gManager.menuOpen = true;
                //Debug.Log(gManager);
            }
            //_hasTower = true;
        }

        public void towerSpawn()
        {
            
            Vector3 pos = gameObject.transform.position;
            pos.y = pos.y + 0.7f;
            if(gManager.Gold >= 10 && !_hasTower)
            {
                gManager.SpendGold(10);
                GameObject tower = Instantiate(Resources.Load("Tower"), pos, new Quaternion(), gameObject.transform.parent) as GameObject;
                tower.transform.parent = gameObject.transform;
                _hasTower = true;
                gManager.menuOpen = false;
                bMenu.SetActive(false);
            }
            else if (gManager.Gold < 10)
            {
                StartCoroutine(noMoney());
            }
            else
            {
                StartCoroutine(isBuilt());
            }
            
        }

        public void sellTower()
        {

            if (_hasTower)
            {
                foreach(Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                gManager.AddGold(9);
                gManager.menuOpen = false;
                bMenu.SetActive(false);
                _hasTower = false;
                
            }

        }


        IEnumerator noMoney()
        {
            fText.SetActive(true);
            yield return new WaitForSeconds(3);
            fText.SetActive(false);
        }
        
        IEnumerator isBuilt()
        {
            tText.SetActive(true);
            yield return new WaitForSeconds(3);
            tText.SetActive(false);
        }

    }
}
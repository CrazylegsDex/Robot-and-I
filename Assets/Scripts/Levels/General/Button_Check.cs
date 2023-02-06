using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PseudoLevels
{
    public class Button_Check : MonoBehaviour
    {
        public float checkRadius;
        public Transform boxCheck;
        public LayerMask boxObjects;
        public GameObject[] boxTests;
        private bool nearBox;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            nearBox = Physics2D.OverlapCircle(boxCheck.position, checkRadius, boxObjects);
            boxTests = GameObject.FindGameObjectsWithTag("Box");
            foreach (GameObject go in boxTests)
            {
                float boxPos = Vector3.Distance(transform.position, go.transform.position);
                if (boxPos / 100 >= .19 && boxPos / 100 <= 0.25)
                {

                    if (go.name.StartsWith("1")) {
                        Debug.Log("Correct!");
                    }

                }
            }
        }
    }
}

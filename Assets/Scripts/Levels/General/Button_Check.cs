// This script allows Baskets to detect if they have an object in them, they can be picky
//
// Author: Robot and I Team
// Last modification date: 02-28-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PseudoLevels
{
    public class Button_Check : MonoBehaviour
    {
        //public float checkRadius;
        //public Transform boxCheck;
        //public LayerMask boxObjects;
        public GameObject[] boxTests;
        public GameObject leftArm;
        public GameObject rightArm;
        public string boxFirstName;
        public int count;

        private bool nearBox;
        public bool complete = false;
        private int num;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            num = 0;
            //nearBox = Physics2D.OverlapCircle(boxCheck.position, checkRadius, boxObjects);
            boxTests = GameObject.FindGameObjectsWithTag("Box");
            foreach (GameObject go in boxTests)//Searches for Gameobject with "Box" tag
            {
                float boxPos = go.transform.position.x;
                float leftPos = leftArm.transform.position.x;
                float rightPos = rightArm.transform.position.x;
                //Debug.Log(leftPos);
                if (leftPos < boxPos && boxPos < rightPos)
                {

                    if (go.name.StartsWith(boxFirstName)) {//Tests for correct first name. 
                        num = num + 1;//counter variable
                    }

                }
            }
            if (num == count)
            {
                complete = true;
                
            }
            else
                complete = false;
        }
    }
}

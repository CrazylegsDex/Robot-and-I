// This script detects/identifies objects in front of bit
//
// Author: Robot and I Team
// Last modification date: 02-28-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bit_Focus
{
    public class Bit_Focus : MonoBehaviour
    {
        private bool isObj = false;
        private GameObject go;

        // Start is called before the first frame update
        void Start()
        {

        }

        void OnTriggerEnter2D()
        {

        }

        void OnTriggerStay2D()
        {

        }

        void OnTriggerExit2D ()
        {

        }

        private bool IsObject ()
        {
            return isObj;
        }
    }
}

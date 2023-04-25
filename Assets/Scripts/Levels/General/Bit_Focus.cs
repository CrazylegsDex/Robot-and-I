// This script detects/identifies objects in front of bit
//
// Author: Robot and I Team
// Last modification date: 03-08-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControl
{
    public class Bit_Focus : MonoBehaviour
    {
        private static List<GameObject> whatCanSee = new List<GameObject>();
        //private int whatDo = 0;
        private static bool Grabbable = false;
        private static int pos;
        private static GameObject go;

        void OnTriggerEnter2D (Collider2D other)
        {
            if (other.gameObject.tag == "Grabbable" && !whatCanSee.Contains(other.gameObject))
            {
                whatCanSee.Add(other.gameObject);
                if (whatCanSee.Count > 0)
                {
                    Grabbable = true;
                }
            }
        }

        void OnTriggerExit2D (Collider2D other)
        {
            if (whatCanSee.Contains(other.gameObject))
            {
                whatCanSee.Remove(other.gameObject);
                if (whatCanSee.Count == 0)
                {
                    Grabbable = false;
                }
            }
        }

        public static bool IsGrabbable()
        {
            return Grabbable;
        }

        public static GameObject WhatGrab()
        {
            pos = whatCanSee.Count;
            go = whatCanSee[pos - 1];
            return go;
        }
    }
}

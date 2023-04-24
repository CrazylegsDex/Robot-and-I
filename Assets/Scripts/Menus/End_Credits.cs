// This script allows control of Bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 04-23-2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndCredits
{
    public class End_Credits : MonoBehaviour
    {
        public GameObject creditEnder;
        // Update is called once per frame
        void Update()
        {
            if (Input.anyKey)
            {
                creditEnder.SetActive(true);
            }
        }
    }
}

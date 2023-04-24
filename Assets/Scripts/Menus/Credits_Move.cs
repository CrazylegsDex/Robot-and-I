// This script allows control of Bit to the player in the platformer view
//
// Author: Robot and I Team
// Last modification date: 04-23-2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndCredits
{
    public class Credits_Move : MonoBehaviour
    {
        public GameObject creditEnder;

        private RectTransform rt;
        private float creditPos = -1700f;
        //private float timeChange = 0f;
        // Start is called before the first frame update
        void Start()
        {
            rt = GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            rt.anchoredPosition = new Vector2(0f, creditPos);
            creditPos += Time.deltaTime * 100f;
            //creditPos += timeChange;
            if (creditPos >= 4000f)
            {
                creditEnder.SetActive(true);
            }
        }
    }
}

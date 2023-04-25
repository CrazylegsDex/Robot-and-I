// This script causes the camera to move down the screen.
// Upon any button press, this script will return the
// player to the MainMenu. If the credits hits the end,
// this script will return the player to the MainMenu
// after 3 seconds.
//
// Author: Robot and I Team
// Last modification date: 04-23-2023

using UnityEngine;
using System.Collections;

namespace GameMechanics
{
    public class EndingCredits : MonoBehaviour
    {
        // Private variables for the script
        private float creditPos = -1700f;
        private bool continueScrolling = true;
        private RectTransform rt;

        // Start gets the RectTransform for the screen
        void Start()
        {
            rt = GetComponent<RectTransform>();
        }

        // Update scrolls the screen and checks for:
        // 1. Button press
        // 2. End of Credits
        void Update()
        {
            if (continueScrolling)
            {
                // Scroll the screen
                rt.anchoredPosition = new Vector2(0f, creditPos);
                creditPos += Time.deltaTime * 100f;
            }

            // If we hit the end of Credits, stop scrolling
            // and start Timer to end scene.
            if (creditPos >= 5600f) // Magic Number. Trust Me
            {
                continueScrolling = false;
                StartCoroutine(Timer());
            }

            // Any Button Press or Click ends the Credits
            if (Input.anyKey)
            {
                StartCoroutine(Load_Level.SceneLoader("MainMenu"));
            }
        }

        // This coroutine waits for 3 seconds and then
        // Loads the MainMenu
        private IEnumerator Timer()
        {
            yield return new WaitForSecondsRealtime(3f);
            StartCoroutine(Load_Level.SceneLoader("MainMenu"));
        }
    }
}

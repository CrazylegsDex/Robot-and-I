// This script allows us to stop player movement when
// a TMP object is selected in Unity
//
// Author: Robot and I Team
// Last modification date: 10-28-2022

using UnityEngine;

namespace PlayerControl
{
    public class TMP_Selection : MonoBehaviour
    {
        // Private variables
        private static bool isTyping = false;

        // Get the current value of typing
        public static bool GetTyping()
        {
            return isTyping;
        }

        // Manually called when the user selects/is typing in the TMP object
        public void SetTyping()
        {
            isTyping = true;
        }

        // Manually called when the user deselects the TMP object
        public void ResetTyping()
        {
            isTyping = false;
        }
    }
}

/* This script is the supplimentary to the
 * Level_Dialogue script. This script is
 * attached to the script controller, and is
 * called by the "Runtime OnClick" methods
 * to call the Start, Continue and Back methods.
 * 
 * Author: Robot and I Team
 * Date Modification: 04-30-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Screen_Dialogue : MonoBehaviour
    {
        // Call the StartDialogue in Level_Dialogue Script
        public void StartButton()
        {
            // Loop through all the attached scripts looking for the correct one
            // If there is only one script attached, continue immediately
            foreach (Level_Dialogue script in FindObjectsOfType<Level_Dialogue>())
            {
                // If the Dialogue Object is currently active
                if (script.DialogueObject.activeSelf)
                {
                    script.StartDialogue();
                    break;
                }
            }
        }

        // Call the DisplayNextSentence in Level_Dialogue Script
        public void ContinueButton()
        {
            // Loop through all the attached scripts looking for the correct one
            // If there is only one script attached, continue immediately
            foreach (Level_Dialogue script in FindObjectsOfType<Level_Dialogue>(true))
            {
                // If the Dialogue Object is currently active
                if (script.DialogueObject.activeSelf)
                {
                    script.DisplayNextSentence();
                    break;
                }
            }
        }

        // Call the DisplayPreviousSentence in Level_Dialogue Script
        public void BackButton()
        {
            // Loop through all the attached scripts looking for the correct one
            // If there is only one script attached, continue immediately
            foreach (Level_Dialogue script in FindObjectsOfType<Level_Dialogue>(true))
            {
                // If the Dialogue Object is currently active
                if (script.DialogueObject.activeSelf)
                {
                    script.DisplayPreviousSentence();
                    break;
                }
            }
        }
    }
}
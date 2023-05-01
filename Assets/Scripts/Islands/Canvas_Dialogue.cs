/* This script is the supplimentary to the
 * Island_Dialogue script. This script is
 * attached to the script controller, and is
 * called by the "Runtime OnClick" methods
 * to call the Back, Continue and Start methods.
 * 
 * Author: Robot and I Team
 * Date Modification: 12-24-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Canvas_Dialogue : MonoBehaviour
    {
        // Call the StartLevel in Island_Dialogue Script
        public void StartButton()
        {
            // Loop through all the attached scripts looking for the correct one
            foreach (Island_Dialogue script in FindObjectsOfType<Island_Dialogue>())
            {
                if (script.isActive)
                {
                    script.StartLevel();
                    break;
                }
            }
        }

        // Call the DisplayNextSentence in Island_Dialogue Script
        public void ContinueButton()
        {
            // Loop through all the attached scripts looking for the correct one
            foreach (Island_Dialogue script in FindObjectsOfType<Island_Dialogue>())
            {
                if (script.isActive)
                {
                    script.DisplayNextSentence();
                    break;
                }
            }
        }

        // Call the CloseDialogue in Island_Dialogue Script
        public void CloseButton()
        {
            // There is no need to check for the correct script
            // attached to the NPC. All this function does is
            // close the animation box, and can be accomplished by
            // any of the attached scripts.
            FindObjectOfType<Island_Dialogue>().CloseDialogue();
        }
    }
}
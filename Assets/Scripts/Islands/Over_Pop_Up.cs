/* This script operates 
 * the Overworld Popup menu.
 * 
 * Robot and I team
 * 11-11-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Over_Pop_Up : MonoBehaviour
    {
        public void QuitToTitle()
        {
            StartCoroutine(Load_Level.SceneLoader("MainMenu"));
        }
    }
}
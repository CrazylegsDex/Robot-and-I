/* This script is used on the main menu
 * Its design is to act upon button presses
 * for each respective menu option
 * 
 * Robot and I team
 * 11-01-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Main_menu : MonoBehaviour
    {
        public void NewGame()
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(Load_Level.SceneLoader("PseudoIsland"));
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}

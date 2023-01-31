/* This script operates 
 * the Overworld Popup menu.
 * 
 * Robot and I team
 * 11-15-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Over_Pop_Up : MonoBehaviour
    {
        // link with MenuButton in OverworldOverlay
        public void PauseWorld()
        {
            Time.timeScale = 0;
        }

        // link with BackButton and ExitWorldButton in MainMenu in LevelMenuOverlay
        public void UnpauseWorld()
        {
            Time.timeScale = 1;
        }

        // link with ExitLevelButton in MainMenu in LevelMenuOverlay
        public void QuitToTitle()
        {
            StartCoroutine(Load_Level.SceneLoader("MainMenu"));
        }
    }
}
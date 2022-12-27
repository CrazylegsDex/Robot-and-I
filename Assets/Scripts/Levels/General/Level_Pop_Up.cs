/* This script operates the Level
 * Popup menu and overlay.
 * 
 * This Script will be used in each
 * level's ScriptController for use
 * it's LevelOverlay and LevelMenuOverlay
 * 
 * Robot and I team
 * 11-15-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class Level_Pop_Up : MonoBehaviour
    {
        public string ThisLocationLevel; // fill with current scene name
        public string QuitLocationLevel; // fill with level's associated overworld

        // link with ResetButton in LevelOverlay
        public void ResetLevel()
        {
            StartCoroutine(Load_Level.SceneLoader(ThisLocationLevel));
        }

        // link with MenuButton in LevelOverlay
        public void PauseLevel()
        {
            Time.timeScale = 0;
        }

        // link with BackButton and ExitLevelButton in MainMenu in LevelMenuOverlay
        public void UnpauseLevel()
        {
            Time.timeScale = 1;
        }

        // link with ExitLevelButton in MainMenu in LevelMenuOverlay
        public void QuitToWorld()
        {
            StartCoroutine(Load_Level.SceneLoader(QuitLocationLevel));
        }
    }
}
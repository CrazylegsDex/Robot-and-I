/* This script operates 
 * the Level Popup menu.
 * 
 * Robot and I team
 * 11-11-2022
 */

using UnityEngine;

namespace GameMechanics
{
    public class LevelPopUpScript : MonoBehaviour
    {
        public void QuitToWorld()
        {
            StartCoroutine(Load_Level.SceneLoader("OverworldPopupTest"));
        }
    }
}
/* This script is used on the main menu
 * Its design is to act upon button presses
 * for each respective menu option
 * 
 * Robot and I team
 * 01/23/2023
 */

using UnityEngine;
using UnityEngine.UI;

namespace GameMechanics
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Menu Buttons")]
        public Button newGameButton;
        public Button continueGameButton;
        public Button optionsGameButton;
        public Button quitGameButton;

        private void Start()
        {
            // Check if there is saved data.
            // If there is none, disable the continue button
            if (!DataPersistenceManager.Instance.HasGameData())
            {
                continueGameButton.interactable = false;
            }
        }

        public void NewGame()
        {
            // Disable Menu Buttons before scene load
            DisableMenuButtons();

            // Create a new game and initialize our game data
            DataPersistenceManager.Instance.NewGame();

            // Initialization is required if there was already a file of data.
            // NewGame sets defaults, below variables will overwrite data
            // Reset these variables to defaults for true NewGame()
            Load_Level.BitLocation = new Vector3(1001.57f, 532.18f, 0f); // x, y, z position of Bit at start new game
            Load_Level.ContinueGame = "PseudoIsland"; // Starting Island/Scene for Bit

            // Use a coroutine to load the Scene in the background
            StartCoroutine(Load_Level.SceneLoader("PseudoIsland"));
        }

        public void ContinueGame()
        {
            // Disable Menu Buttons before scene load
            DisableMenuButtons();

            // Continue the game using previous save data
            StartCoroutine(Load_Level.SceneLoader("ContinueGame"));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        // This function disables all MenuButtons.
        // Prevents doubleclicking of a button and causing
        // unintended consequences.
        private void DisableMenuButtons()
        {
            // Disable all buttons while the game loads
            newGameButton.interactable = false;
            continueGameButton.interactable = false;
            optionsGameButton.interactable = false;
            quitGameButton.interactable = false;
        }
    }
}

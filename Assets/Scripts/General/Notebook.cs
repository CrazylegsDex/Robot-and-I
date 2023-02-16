/*
 * This class adds a notebook feature to our game.
 * This notebook will be a text gui for the player
 * to put in notes for what they have learned.
 * Updates persist through closing the notebook
 * menu.
 * 
 * Author: Robot and I Team
 * Date: 02/07/2023
 */

using UnityEngine;
using TMPro;

namespace GameMechanics
{
    public class Notebook : MonoBehaviour, DataPersistenceInterface
    {
        public TMP_InputField Notes;
        private bool SetStatus = false;

        // This function pulls data from the GameData class
        public void LoadData(GameData data)
        {
            // Load the text from the dictionary into the notebook
            Notes.text = data.notebook;
        }

        // This function saves local data to persist through the game
        public void SaveData(GameData data)
        {
            // Save the current notebook text into the dictionary
            data.notebook = Notes.text;
        }

        // This function is used to save the text in the notebook
        public void SaveText()
        {
            // Call the SaveGame function from the DataPersistenceManager
            DataPersistenceManager.Instance.SaveGame();
        }

        // Public function to set the notebook either visible or invisible
        public void SetVisiblityStatus()
        {
            SetStatus = !SetStatus; // Invert the status variable
            Notes.gameObject.SetActive(SetStatus); // Set the status
        }
    }
}
/*
 * This class adds a notebook feature to our game.
 * This notebook will be a text gui for the player
 * to put in notes for what they have learned.
 * Updates persist through closing the notebook
 * menu.
 * 
 * Author: Robot and I Team
 * Date: 02/02/2023
 */

using UnityEngine;
using TMPro;

namespace GameMechanics
{
    public class Notebook : MonoBehaviour, DataPersistenceInterface
    {
        public TMP_InputField Notes;

        // This function pulls data from the GameData class
        public void LoadData(GameData data)
        {
            // Placeholder code
            Notes.text = data.notebook;
        }

        // This function saves local data to persist through the game
        public void SaveData(GameData data)
        {
            // Placeholder code
            data.notebook = Notes.text;
        }

        // This function is used to save the text in the notebook
        public void SaveText()
        {
            DataPersistenceManager.Instance.SaveGame();
        }
    }
}
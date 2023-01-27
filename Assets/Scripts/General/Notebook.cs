/*
 * This class adds a notebook feature to our game.
 * This notebook will be a text gui for the player
 * to put in notes for what they have learned.
 * Updates persist through closing the notebook
 * menu.
 * 
 * Author: Robot and I Team
 * Date: 01/23/2023
 */

using UnityEngine;

namespace GameMechanics
{
    public class Notebook : MonoBehaviour, DataPersistenceInterface
    {
        private string playerNotes; // Temp placeholder

        // This function pulls data from the GameData class
        public void LoadData(GameData data)
        {
            // Placeholder code
            playerNotes = data.notebook;
        }

        // This function saves local data to persist through the game
        public void SaveData(GameData data)
        {
            // Placeholder code
            data.notebook = playerNotes;
        }
    }
}
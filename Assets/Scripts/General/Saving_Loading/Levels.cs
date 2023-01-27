/*
 * This script handles the marking of completion for
 * the levels. This script will talk to IslandNPC through
 * the use of DataPersistenceInterface and the loading/saving
 * of the levels to the file.
 * 
 * Author: Robot and I Team
 * Date: 01/27/2023
 */

using UnityEngine;

namespace GameMechanics
{
    public class Levels : MonoBehaviour, DataPersistenceInterface
    {
        // Public variables
        public string LevelToLoad;
        public string LevelToComplete;

        // Private Variables
        private bool Completed;

        // Send the player back to the overworld and set level as complete
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionGameObject is the first object that moved into the second
            // Effectively refers to the object that just ran into the object this script is attached.
            GameObject collisionGameObject = collision.gameObject;

            if (collisionGameObject.CompareTag("Player")) // Did the "Player" collide into the current object
            {
                // Set that this level was completed
                Completed = true;

                // Use a coroutine to load the Scene in the background
                StartCoroutine(Load_Level.SceneLoader(LevelToLoad));
            }
        }
        
        // Data persistence implementation of LoadGame
        public void LoadData(GameData data)
        {
            // Load the current levels completed status
            Completed = data.gameLevels[LevelToComplete];
        }

        // Data persistence implementation of SaveGame
        public void SaveData(GameData data)
        {
            // If the player completed this level by hitting the end
            // NPC, save the completion to the file
            if (data.gameLevels.ContainsKey(LevelToComplete))
            {
                data.gameLevels[LevelToComplete] = Completed;
            }
        }
    }
}
/* This script operates the
 * progress bar in the overworld
 * showing the total game progress
 * 
 * Robot and I team
 * 02-06-2023
 */

using UnityEngine;
using UnityEngine.UI;

namespace GameMechanics
{
    public class ProgressBar : MonoBehaviour, DataPersistenceInterface
    {
        public int MaximumLevels; // Holds the maximum number of levels to complete
        public Image Mask; // The masking layer for the progress bar
        private int CurrentLevels = 0; // Holds the current number of levels completed

        // DataPersistenceInterface implementation of LoadData
        public void LoadData(GameData data)
        {
            // Check if the dictionary has been filled with data
            if (data.gameLevels.ContainsKey("PS_Level1"))
            {
                // Loop through the keys in the dictionary
                foreach (var key in data.gameLevels)
                {
                    if (key.Value) // If the key value is true (level completed)
                    {
                        ++CurrentLevels;
                    }
                }
            }

            // Fill the Progress Bar
            Mask.fillAmount = (float) CurrentLevels / MaximumLevels;
        }

        // DataPersistenceinterface implementation of SaveData
        public void SaveData(GameData data)
        {
            // Implementation of function required for Interface
            // No data needs to be saved. Function will remain empty
        }
    }
}
/*
 * This script handles the unlocking of the levels for the game.
 * The unlocking for the levels will follow a pattern.
 * The player will start on PseudoIsland and only Level 1 will
 * be available. Other levels and ships will not be available.
 * Once the player completes Level 1, then the ships will appear
 * and Level 1 will be available on Python and C# Islands along
 * with Level 2 on PseudoIsland.
 * From here on, completing a level on PseudoIsland will unlock
 * the next level in sequence for PseudoIsland. Completing a level
 * in either Python or C# will unlock the next level in sequence iff
 * the current level in sequence is also complete for PseudoIsland.
 * 
 * Author: Robot and I Team
 * Date: 01/27/2023
 */

using UnityEngine;

namespace GameMechanics
{
    public class IslandNPC : MonoBehaviour, DataPersistenceInterface
    {
        // Unique Level ID and private bool to mark level complete
        public string id;
        private bool markComplete;
        private bool checkComplete;

        // Data persistence implementation of LoadGame
        // This function will set active those levels in the game
        // that should be active according to the implementation
        // specifications. All other levels are inactive to the player.
        public void LoadData(GameData data)
        {
            /* To understand the following code, here is a brief explanation
             * of what is going on with the Dictionary, Save File/LoadData and Unity.
             * 
             * In Unity, this script is attached to many different NPC. When Unity
             * runs, each script (NPC) is unique and therefore using the variable
             * id or this.gameObject will refer only to the current script that
             * Unity is referencing at that time.
             * For the Dictionary, when the player first starts the game, the
             * dictionary is empty and any calls to a key will be an error that
             * the key does not exist. Once the game is saved for the first time,
             * the dictionary will be populated with all the keys in the current scene,
             * and each NPC will be able to check another NPC's value in the dictionary.
             * Example: NPC1 can check the markComplete bool for NPC25
             * When LoadData runs, it loops through (randomly) all NPC (in the current scene)
             * with this script attached. So this function will have a unique id and
             * a unique gameObject each time it runs through the following code.
             * This code takes advantage of the above by checking other NPC "markComplete" values,
             * and if it is true, the following code will set their gameObject to active
             * when it is their turn to pass through this function.
             */

            // Try to get an id from the dictionary. If successful, set the
            // current NPC's markComplete variable
            data.gameLevels.TryGetValue(id, out markComplete);

            // Always will be true that the first level on every Island is active
            // The ships to PY and CS are only active once PS_Level1 is complete
            if (id == "PS_Level1" || id == "PY_Level1" || id == "CS_Level1" || id == "Level_0")
            {
                gameObject.SetActive(true);
            }
            else
            {
                // Otherwise, all NPC are set inactive/unplayable
                gameObject.SetActive(false);
            }

            // If PS_Level1 is completed, set the ships active
            if (data.gameLevels.ContainsKey("PS_Level1")) // If we can access the key yet (first save)
            {
                if (data.gameLevels["PS_Level1"]) // If level 1 is complete
                {
                    if (id == "PS_Level1") // On level 1's turn, change the dialogue
                    {
                        // Get an object of the Level1 NPC
                        Island_Dialogue NPC = GameObject.Find("Sally").GetComponent<Island_Dialogue>();

                        // Change the Dialogue that the NPC presents.
                        NPC.sentences[0] = "This is a test";
                        NPC.sentences[1] = "I am testing more";
                        NPC.sentences[2] = "MASHED POTATOES!!!!";
                    }
                    if (id == "Ship1" || id == "Ship2") // On the Ship's turn, set them active
                    {
                        gameObject.SetActive(true);
                    }
                }
            }

            // For PseudoIsland, if Level is complete, set Level + 1 to active
            if (data.gameLevels.ContainsKey("PS_Level1")) // Access the key values yet
            {
                // Ignore Python and CSharp NPC
                if (id.Contains("PS_Level") && !id.Contains("Ship"))
                {
                    // Loop through all the levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("PS_Level" + i, out checkComplete);

                        if (checkComplete) // If level exists and was complete, set next level
                        {
                            if (id == ("PS_Level" + (i + 1))) // Next level id with current
                            {
                                gameObject.SetActive(true);
                                break; // Only one level will open each completion
                            }
                        }
                    }
                }
            }

            // For PythonIsland and CIsland, if Level is complete and the equivalent
            // Pseudo level is complete, then set Level + 1 to active
            if (data.gameLevels.ContainsKey("PY_Level1") || data.gameLevels.ContainsKey("CS_Level1"))
            {
                // Separate out the Python and CSharp NPC based on ID
                if (id.Contains("PY_Level") && !id.Contains("Ship"))
                {
                    // Loop through the Python Levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("PS_Level" + i, out checkComplete);

                        if (checkComplete) // PS Level exists and was complete
                        {
                            // Test if level exists
                            data.gameLevels.TryGetValue("PY_Level" + i, out checkComplete);

                            if (checkComplete) // PY Level exists and was complete
                            {
                                if (id == ("PY_Level" + (i + 1))) // Next level id with current
                                {
                                    gameObject.SetActive(true);
                                    break; // Only one level will open each completion
                                }
                            }
                        }
                    }
                }

                if (id.Contains("CS_Level") && !id.Contains("Ship"))
                {
                    // Loop through the CSharp Levels
                    for (int i = 1; i <= data.gameLevels.Count; ++i)
                    {
                        // Test if level exists
                        data.gameLevels.TryGetValue("PS_Level" + i, out checkComplete);

                        if (checkComplete) // PS Level exists and was complete
                        {
                            // Test if level exists
                            data.gameLevels.TryGetValue("CS_Level" + i, out checkComplete);

                            if (checkComplete) // CS Level exists and was complete
                            {
                                if (id == ("CS_Level" + (i + 1))) // Next level id with current
                                {
                                    gameObject.SetActive(true);
                                    break; // Only one level will open each completion
                                }
                            }
                        }
                    }
                }
            }
        }

        // Data persistence implementation of SaveGame
        public void SaveData(GameData data)
        {
            // Add the level to the Dictionary when the game is saved.
            // Only add levels that are new. New levels appear when the
            // player arrives in a new scene that he has not been in before
            if (!data.gameLevels.ContainsKey(id))
            {
                data.gameLevels.Add(id, markComplete);
            }
        }
    }
}
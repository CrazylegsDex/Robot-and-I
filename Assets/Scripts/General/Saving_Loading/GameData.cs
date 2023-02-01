/*
 * This class stores the state of our Game Data.
 * Any variables that need to be persisted throughout the
 * game will be held and updated here.
 * 
 * Author: Robot and I Team
 * Date: 01/23/2023
 */

using UnityEngine;

namespace GameMechanics
{
    // System.Serializable means that this class is able to be Serialized
    // To Serialize something means that it can be compressed from C#
    // code into another format (like JSON which is what our Game Data
    // will be saved into).
    [System.Serializable]
    public class GameData
    {
        // These variables will be saved into the game file
        public Vector3 playerPosition;
        public string playerScene;
        public SerializableDictionary<string, bool> gameLevels;
        public string notebook;

        // Class Constructor. When player hits "NewGame" these are the default
        // values that the game will start with
        public GameData()
        {
            playerPosition = new Vector3(1001.57f, 532.18f, 0f); // x, y, z position of Bit at start new game
            playerScene = "PseudoIsland"; // Starting Island/Scene for Bit
            gameLevels = new SerializableDictionary<string, bool>(); // Originally Empty dictionary
            notebook = "";
        }
    }
}
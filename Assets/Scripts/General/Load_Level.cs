/* This script allows us to change scenes based on
 * the information in the scene manager.
 * 
 * Author: Robot and I team
 * Date Modification: 01/23/2023
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameMechanics
{
    public class Load_Level : MonoBehaviour, DataPersistenceInterface
    {
        // Hidden variables
        [HideInInspector] public static Vector3 BitLocation;
        [HideInInspector] public static string ContinueGame;

        // LoadData implementation for the DataPersistenceInterface
        public void LoadData(GameData data)
        {
            // If a previous scene was found, load the previous scene
            ContinueGame = data.playerScene;
            BitLocation = data.playerPosition; // Prevent overwrite of playerPosition in SaveData
        }

        // SaveData implementation for the DataPersistenceInterface
        public void SaveData(GameData data)
        {
            // Save the current scene to the playerScene variable (Do not save MainMenu)
            if (ContinueGame != "MainMenu")
            {
                data.playerScene = ContinueGame;
            }
            data.playerPosition = BitLocation; // Update position only if he took a ship, else same as data.playerPosition
        }

        // Static method available to be used as a coroutine SceneLoader
        // Bool parameter is used to determine if the NPC teleporting Bit is a Ship NPC
        // In this case, modify Bit's teleporting location to the correct Ship Location
        public static IEnumerator SceneLoader(string SceneToLoad, bool ShipNPC = false)
        {
            // If this is a Ship NPC, ShipNPC field will be true
            if (ShipNPC)
            {
                BitLocation = DestinationLocation(ContinueGame, SceneToLoad);
                PlayerControl.TopDown_Movement.position = BitLocation; // Set Bit's new Location
            }

            // Use the ContinueGame variable to keep track of the current scene
            if (SceneToLoad != "ContinueGame")
            {
                ContinueGame = SceneToLoad;
            }

            // Save the current scene before loading the next scene
            DataPersistenceManager.Instance.SaveGame();

            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex.
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ContinueGame);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        // This function returns a Vector3 of the location that Bit will be
        // when he takes a Ship to another Island
        private static Vector3 DestinationLocation(string previousIsland, string destinationIsland)
        {
            // The destination values that Bit will return to on the Island are hardcoded
            // and must be updated if Ship locations change.
            if (previousIsland == "PseudoIsland" && destinationIsland == "PythonIsland")
                return new Vector3(1054.826f, 556.4346f);
            if (previousIsland == "PseudoIsland" && destinationIsland == "CIsland")
                return new Vector3(963.8285f, 523.3897f);
            if (previousIsland == "PythonIsland" && destinationIsland == "PseudoIsland")
                return new Vector3(1011.86f, 580.9894f);
            if (previousIsland == "PythonIsland" && destinationIsland == "CIsland")
                return new Vector3(1044.198f, 559.1546f);
            if (previousIsland == "CIsland" && destinationIsland == "PseudoIsland")
                return new Vector3(922.588f, 611.5881f);
            if (previousIsland == "CIsland" && destinationIsland == "PythonIsland")
                return new Vector3(976.2f, 558.5659f);

            // Based on the check for when this function is called,
            // this statement should never execute. However, if it does,
            // this location is shared between all Islands and will not affect
            // gameplay in any way.
            return new Vector3(1004.09f, 533.2f);
        }
    }
}
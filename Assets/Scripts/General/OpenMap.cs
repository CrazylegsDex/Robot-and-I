using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMechanics
{
	public class OpenMap : MonoBehaviour, DataPersistenceInterface
	{
		[SerializeField] public string mapName;
		[HideInInspector] public static Vector3 BitLocation;
		[HideInInspector] public static string ContinueGame;
		// Update is called once per frame
		
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
		
		void Update()
		{
			if(Input.GetKeyDown(KeyCode.M)){
				DataPersistenceManager.Instance.SaveGame();
				SceneManager.LoadScene(mapName);
			}
		}

	}
}

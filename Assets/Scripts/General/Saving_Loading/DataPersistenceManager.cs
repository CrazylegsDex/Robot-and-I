/*
 * This class is the brain of the Saving and Loading for
 * the game. This class handles when to save to file and when to
 * load from file. This class also handles and keeps track of
 * any scripts that implement the DataPersistence Interface.
 * 
 * Author: Robot and I Team
 * Date: 01/23/2023
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameMechanics
{
    public class DataPersistenceManager : MonoBehaviour
    {
        // File variables
        [Header("File Storage Config")]
        [SerializeField] private string fileName;
        [SerializeField] private bool useEncryption;

        // Autosave time
        private float autoSaveTimeSeconds = 600f; // 10 Minutes

        // Variable to keep track of GameData
        private GameData gameData;

        // Coroutine for the Autosave method
        private Coroutine autoSaveCoroutine;

        // List of all scripts that are using the DataPersistenceInterface
        private List<DataPersistenceInterface> dataPersistenceObjects;

        // File Data Handler
        private FileDataHandler dataHandler;

        // This class is designed to be a "Singleton" class.
        // This means there will be only one instance of this
        // class at all times.
        public static DataPersistenceManager Instance { get; private set; }

        private void Awake()
        {
            // There should only be one instance at any given time
            if (Instance != null)
            {
                // If found a second instance, destroy new one and keep old instance
                Destroy(gameObject);
                return;
            }

            Instance = this; // If there is no current instance, create a singleton instance
            DontDestroyOnLoad(gameObject); // Don't destroy this script on scene transition

            // Assign dataHandler with the path, filename and encryption
            // For details on "Application.persistentDataPath" -> https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html
            dataHandler = new FileDataHandler(fileName, Application.persistentDataPath, useEncryption);
        }

        // This Unity function is called when the object is enabled and active.
        // This function "Subscribes" to the scene loader
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // This Unity function is called when the behavior becomes disabled or inactive
        // This function "UnSubscribes" to the scene loader
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // This function is called whenever a scene is loaded in Unity.
        // By "Subscribing" to the scene manager with the above functions,
        // this function stays current with each scene that is loaded in the game.
        // This function will load all objects in the loaded scene with data
        // that is found in the file if they implement the DataPersistenceInterface.
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Find all objects that implement the interface
            dataPersistenceObjects = FindAllDataPersistenceObjects();
            ContinueGame(); // Fill them with saved data

            // Check autosave coroutine if its running.
            // If it is, reset the timer.
            // If it isn't, start the timer
            if (autoSaveCoroutine != null)
            {
                StopCoroutine(autoSaveCoroutine);
            }
            autoSaveCoroutine = StartCoroutine(AutoSave());
        }

        // When the player clicks NewGame
        public void NewGame()
        {
            // Create a new GameData object
            gameData = new GameData();
        }

        // When the player clicks ContinueGame or when a new scene is loaded
        public void ContinueGame()
        {
            // Load data from the FileDataHandler into the gameData class
            gameData = dataHandler.Load();

            // If there was no game data to load (file doesn't exist), return
            if (gameData == null)
                return;

            // There was game data to load.
            // Push the loaded data to all scripts that implement the interface
            foreach (DataPersistenceInterface dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        // When the player clicks SaveGame, Application Quits or game is AutoSaved.
        public void SaveGame()
        {
            // Trying to save null data will cause an error
            if (gameData == null)
                return;

            // Call all scripts that have implemented the interface and cause them to
            // update the gameData's data
            foreach (DataPersistenceInterface dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(gameData);
            }

            // Now that game data is current, save the data to the file
            dataHandler.Save(gameData);
        }
        
        // Return a list of all scripts that have implemented the DataPersistentInterface
        private List<DataPersistenceInterface> FindAllDataPersistenceObjects()
        {
            // Finds all scripts that have mono behavior and our DataPersistenceInterface implemented.
            // The true parameter for "FindObjectOfType" means include those scripts that are inactive
            IEnumerable<DataPersistenceInterface> dataPersistenceObjects =
                                FindObjectsOfType<MonoBehaviour>(true).OfType<DataPersistenceInterface>();

            return new List<DataPersistenceInterface>(dataPersistenceObjects);
        }

        // Return if there is saved data
        public bool HasGameData()
        {
            return gameData != null;
        }

        // Wait for a timer to expire then save game data
        private IEnumerator AutoSave()
        {
            // Infinite loop
            while (true)
            {
                yield return new WaitForSeconds(autoSaveTimeSeconds);

                // TODO - Replace with SaveGame Popup
                Debug.Log("Saving Game. Please do not close the application.");
                SaveGame();
                Debug.Log("Game has been saved.");
            }
        }

        // When the player exits the game, save game data
        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}
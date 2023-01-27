/*
 * This class compresses the data in GameData into a file format.
 * The file format chosen for this game is a standard JSON file.
 * This class will handle compressing the GameData and sending it to
 * the JSON file as well as handle loading the GameData and reformatting
 * it back into C# GameData.
 * 
 * Author: Robot and I Team
 * Date: 01/23/2023
 */

using UnityEngine;
using System;
using System.IO;

namespace GameMechanics
{
    public class FileDataHandler
    {
        // Private variables for file path and file name
        private string dataFileName = "";
        private string dataDirPath = "";

        // Private variables for the encryption of the Game Save Data
        private bool useEncryption = false;
        private readonly string encryptionKey = "SeniorProjectWestTexasAM";

        // Constructor initializes local variables
        public FileDataHandler(string name, string path, bool encrypt)
        {
            dataFileName = name;
            dataDirPath = path;
            useEncryption = encrypt;
        }

        // Load/Read from the file
        public GameData Load()
        {
            // File path concatenation for OS independent pathing
            string fullPath = Path.Combine(dataDirPath, dataFileName);
            GameData loadedData = null;

            // Check if file exists
            if (File.Exists(fullPath))
            {
                try
                {
                    // Read in the file
                    string dataToLoad = "";

                    // Using statements to create a FileStream and StreamReader
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    // If the data was encrypted
                    if (useEncryption)
                    {
                        dataToLoad = EncryptDecrypt(dataToLoad);
                    }

                    // The data is still in JSON format. Convert to C# format - Deserialize
                    loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Error occurred when trying to read from file.\nPath: " + fullPath + "\nException: " + e);
                }
            }

            return loadedData;
        }

        // Save to the file
        public void Save(GameData data)
        {
            // File path concatenation for OS independent pathing
            string fullPath = Path.Combine(dataDirPath, dataFileName);

            try
            {
                // Create the directory if it does not exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                // Serialize the GameData into the JSON file
                string dataToStore = JsonUtility.ToJson(data, true); // True is prettyprint for viewing

                // If we are encrypting the data
                if (useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }

                // Write the serialized data to the file
                // Using block will close file once complete, ensures no dangling open files
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                // Throw an error if something went wrong
                Debug.LogError("Error occurred when trying to save data to file.\nPath: " + fullPath + "\nException: " + e);
            }
        }

        // This method will encrypt and decrypt the file
        private string EncryptDecrypt(string data)
        {
            string encryptedData = "";
            for (int i = 0; i < data.Length; ++i)
            {
                // Simple XOR encryption using the key and the data
                encryptedData += (char) (data[i] ^ encryptionKey[i % encryptionKey.Length]);
            }

            return encryptedData;
        }
    }
}
/*
 * This class is implemented with a single function.
 * This function will be an OnClick implementation
 * inside the Unity Editor and simply opens up the
 * User Manual for the player to read.
 * 
 * Author: Robot and I Team
 * Date: 02/03/2023
 */

using UnityEngine;
using System.IO;

public class Manual : MonoBehaviour
{
    // This function opens the User Manual.pdf
    // document. This location varies based on
    // the Unity Editor and the Unity build.
    public void OpenManual()
    {
        if (Path.DirectorySeparatorChar == '\\') // Windows machine
        {
            string ManualPath;

            if (Application.isEditor) // If in Editor mode
            {
                // Get the Path
                ManualPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "BuildRelease\\UserManual.pdf");

                // Check the validity of the file before attempting an open
                if (!File.Exists(ManualPath))
                    throw new FileNotFoundException("Manual was not found at: " + ManualPath);

                // Open the PDF document
                Application.OpenURL("file:" + ManualPath);
            }
            else // If in Build mode
            {
                // Get the Path
                ManualPath = Path.Combine(Path.GetDirectoryName(Application.dataPath), "UserManual.pdf");

                // Check the validity of the file before attempting an open
                if (!File.Exists(ManualPath))
                    throw new FileNotFoundException("Manual was not found at: " + ManualPath);

                // Open the PDF document
                Application.OpenURL("file:" + ManualPath);
            }
        }
    }
}

/* This script allows us to change scenes based on
 * the information in the scene manager.
 * 
 * Author: Robot and I team
 * Date Modification: 10/28/2022
 */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMechanics
{
    public class Load_Level : MonoBehaviour
    {
        // Public variables
        public int iLevelToLoad; // For the integer version
        public string sLevelToLoad; // For the name version
        public bool userIntegerToLoadLevel = false;

        // Will trigger when two objects collide
        // Parameter is the object that the script is attached to (auto passed)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionGameObject is the first object that moved into the second
            // Effectively refers to the object that just ran into the object this script is attached.
            GameObject collisionGameObject = collision.gameObject;

            if (collisionGameObject.tag == "Player") // Did the "Player" collide into the current object
            {
                LoadScene();
            }

            // Leave as a function for future updates to loading a scene
            void LoadScene()
            {
                if (userIntegerToLoadLevel) // Should we use the integer input
                {
                    SceneManager.LoadScene(iLevelToLoad);
                }
                else // Use the String level name
                {
                    SceneManager.LoadScene(sLevelToLoad);
                }
            }
        }
    }
}
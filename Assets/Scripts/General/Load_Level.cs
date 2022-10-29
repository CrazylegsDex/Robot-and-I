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
        public int iLevelToLoad;
        public string sLevelToLoad;
        public bool userIntegerToLoadLevel = false;

        // Will trigger when two objects collide
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionGameObject is the first object that moved into the second
            // Effectively refers to the object that just ran into the object this script is attached.
            GameObject collisionGameObject = collision.gameObject;

            if (collisionGameObject.tag == "Player")
            {
                LoadScene();
            }

            void LoadScene()
            {
                if (userIntegerToLoadLevel)
                {
                    SceneManager.LoadScene(iLevelToLoad);
                }
                else
                {
                    SceneManager.LoadScene(sLevelToLoad);
                }
            }
        }
    }
}
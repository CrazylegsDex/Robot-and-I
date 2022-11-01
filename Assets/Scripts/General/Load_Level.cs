/* This script allows us to change scenes based on
 * the information in the scene manager.
 * 
 * Author: Robot and I team
 * Date Modification: 11-01-2022
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameMechanics
{
    public class Load_Level : MonoBehaviour
    {
        // Public variables
        public string LevelToLoad;

        // Will trigger when two objects collide
        // Parameter is the object that the script is attached to (auto passed)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionGameObject is the first object that moved into the second
            // Effectively refers to the object that just ran into the object this script is attached.
            GameObject collisionGameObject = collision.gameObject;

            if (collisionGameObject.tag == "Player") // Did the "Player" collide into the current object
            {
                // Use a coroutine to load the Scene in the background
                StartCoroutine(SceneLoader(LevelToLoad));
            }
        }

        // Static method available to be used as a coroutine SceneLoader
        public static IEnumerator SceneLoader(string SceneToLoad)
        {
            // The Application loads the Scene in the background as the current Scene runs.
            // This is particularly good for creating loading screens.
            // You could also load the Scene by using sceneBuildIndex.

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneToLoad);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
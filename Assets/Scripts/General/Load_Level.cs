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
		
		public void startLevel(){
			StartCoroutine(SceneLoader(LevelToLoad));
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
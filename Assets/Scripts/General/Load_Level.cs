using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if(collisionGameObject.tag == "Player")
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

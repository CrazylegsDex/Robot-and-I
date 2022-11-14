using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue_Trigger : MonoBehaviour{
	public Dialogue dialogue;
	
	public void TriggerDialogue(){
		FindObjectOfType<Dialogue_Manger>().startDialogue(dialogue);
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionGameObject is the first object that moved into the second
            // Effectively refers to the object that just ran into the object this script is attached.
            GameObject collisionGameObject = collision.gameObject;

            if (collisionGameObject.tag == "Player") // Did the "Player" collide into the current object
            {
                // Use a coroutine to load the Scene in the background
                FindObjectOfType<Dialogue_Manger>().startDialogue(dialogue);
            }
        }

}

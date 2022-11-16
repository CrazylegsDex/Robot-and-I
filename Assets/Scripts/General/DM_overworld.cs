using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DM_overworld : MonoBehaviour
{
	// Public and private Varibles
    public TextMeshProUGUI nametext;
	public TextMeshProUGUI dialoguetext;
	public TextMeshProUGUI levelToLoadtext;
	private Queue<string> sentences;
	public Animator animationDialogueBox;
	
	// Assigns sentences to a queue
    void Start()
    {
        sentences = new Queue<string>();
    }
	
	// when player runs into npc this method starts the dialogue
	public void startDialogue(DialogueOver dialogue){
		// brings dialogue box where player can see
		animationDialogueBox.SetBool("isOpen", true);
		// next two lines assigns where they move to in the dialogue box
		nametext.text = dialogue.name;
		levelToLoadtext.text = dialogue.LevelToLoad;
		// clears any thing left in dialogue box
		sentences.Clear();
		//Enqueues each sentence
		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}
		
		// display for first sentence
		displayNextSentence();
		
	}
	
	// this method is used when user hits continue button
	public void displayNextSentence(){
		if( sentences.Count == 0){
			//endDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		dialoguetext.text = sentence;
	}
	
	// this is activated when player hits the start button
	public void startLevel(){
		endDialogue();
		string levelToLoad = levelToLoadtext.text;
		StartCoroutine(SceneLoader(levelToLoad));
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
	// this activates when start and back button are pressed and ended the 
	// dialogue and moves the box out of the way
	public void endDialogue(){
		animationDialogueBox.SetBool("isOpen", false);
	}
}

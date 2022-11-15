using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DM_overworld : MonoBehaviour
{
    public TextMeshProUGUI nametext;
	public TextMeshProUGUI dialoguetext;
	public TextMeshProUGUI levelToLoadtext;
	private Queue<string> sentences;
	
    void Start()
    {
        sentences = new Queue<string>();
    }
	
	public void startDialogue(DialogueOver dialogue){
		
		
		nametext.text = dialogue.name;
		levelToLoadtext.text = dialogue.LevelToLoad;
		sentences.Clear();
		
		foreach(string sentence in dialogue.sentences){
			sentences.Enqueue(sentence);
		}
		
		displayNextSentence();
	}
	
	public void displayNextSentence(){
		if( sentences.Count == 0){
			endDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		dialoguetext.text = sentence;
	}
	
	
	public void startLevel(){
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
	void endDialogue(){
		
	}
}

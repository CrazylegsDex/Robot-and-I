using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Complete_check : MonoBehaviour
{
    public GameObject gameobject;

    public void ReadString()
    {
        string path = Application.dataPath + "/Scripts/test.txt";
        StreamReader reader = new StreamReader(path);
        string word = reader.ReadLine();
        //Read the text from directly from the test.txt file
        Debug.Log(word);
        //Debug.Log(reader.ReadLine());
        if ( word == " 1111" || word == " 111111")
        {
            Debug.Log("works");
            gameobject.SetActive(false); //hides the platform blocking level progress
            File.WriteAllText(path, " ");
        }
        
    }
    
}
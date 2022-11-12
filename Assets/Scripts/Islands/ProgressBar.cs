/* This script operates the
 * progress barin the overworld
 * showing the total game progress
 * 
 * Robot and I team
 * 11-11-2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int max; // Holds max progress amount
    public int cur; // Holds current progress amount
    public Image mask; // Layer that masks

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)cur / (float)max;
        mask.fillAmount = fillAmount;
    }
}

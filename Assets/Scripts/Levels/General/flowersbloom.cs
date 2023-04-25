using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowersbloom : MonoBehaviour
{
	public SpriteRenderer notBloomed;
	public Sprite bloomed;
	public int timesPassed;
	public string color;
	
     private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                timesPassed++;
				if(color == "red" && timesPassed == 2){
					notBloomed.sprite = bloomed;
				}
				else if(color == "blue" && timesPassed == 3){
					notBloomed.sprite = bloomed;
				}else{
					if(color != "red" && color != "blue"){
						notBloomed.sprite = bloomed;
					}
				}
            }
        }
}

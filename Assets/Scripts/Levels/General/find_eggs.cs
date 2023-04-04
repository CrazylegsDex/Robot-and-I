using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class find_eggs : MonoBehaviour
{
	public SpriteRenderer bushWithEgg;
	public Sprite bushWithoutEgg;
	public GameObject lesson2;
	public GameObject scanner;
	public int bushNum;
	
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                bushWithEgg.sprite = bushWithoutEgg;
				if(bushNum == 6){
					lesson2.SetActive(true);
					scanner.SetActive(false);
				}
            }
        }
}

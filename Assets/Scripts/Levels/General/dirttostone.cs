using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirttostone : MonoBehaviour
{
	public SpriteRenderer dirts;
	public Sprite stones;
	public Sprite fences;
	public GameObject arrow1;
	public GameObject arrow2;
	public int num;
	private int passed = 0;
	public GameObject lesson2;
	
     private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                arrow1.SetActive(false);
				if(passed == 0){
					dirts.sprite = stones;
				}
				if(passed == 2){
					dirts.sprite = fences;
				}
				passed++;
				if(num == 8){
					arrow2.SetActive(true);
					lesson2.SetActive(true);
				}
				
            }
        }
}

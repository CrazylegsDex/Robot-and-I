using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeGrass : MonoBehaviour
{
    public SpriteRenderer tallGrass;
	public Sprite shortGrass;
	public int grassCut = 0;
	public BoxCollider2D levelSprite;
	
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                tallGrass.sprite = shortGrass;
				if(grassCut == 37){
					levelSprite.isTrigger = true;
				}
            }
        }
}

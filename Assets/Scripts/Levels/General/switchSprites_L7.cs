using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchSprites_L7 : MonoBehaviour
{
    public SpriteRenderer dirt;
	public Sprite dirtTree;
	public SpriteRenderer dirtReader;
	public Sprite red;
	public Sprite green;
	public int goodOrBad;
	
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(goodOrBad == 1){
					dirt.sprite = dirtTree;
					dirtReader.sprite = green;
				}else{
					dirtReader.sprite = red;
				}
            }
        }
}
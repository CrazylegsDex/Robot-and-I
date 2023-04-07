using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireflys : MonoBehaviour
{
    public GameObject firefly;
	
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                firefly.SetActive(false);
            }
        }
}

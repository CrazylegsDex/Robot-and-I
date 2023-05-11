using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakesorter : MonoBehaviour
{
	public GameObject snakes;
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                snakes.SetActive(true);
            }
        }
}

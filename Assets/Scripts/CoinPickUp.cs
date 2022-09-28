using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinSound;
    [SerializeField] int coinValue = 10;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.tag == "Player")
        {

            FindObjectOfType<GameSession>().UpdateScore(coinValue);

            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);

            Destroy(gameObject);

            
        }

    }


}

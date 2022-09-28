using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    PlayerMovement player;

    [SerializeField] AudioClip coinSound;

    private void Awake() 
    {
        player = FindObjectOfType<PlayerMovement>();    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.tag == "Player")
        {

            
            player.hasBow = true; 
            
            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);

            Destroy(gameObject);

            
        }

    }
}

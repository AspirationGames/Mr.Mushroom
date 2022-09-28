using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    BoxCollider2D arrowCollider;
    Rigidbody2D arrowBody;
    PlayerMovement player;

    float xSpeed;
    [SerializeField] float arrowSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        arrowBody = GetComponent<Rigidbody2D>();
        arrowCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        arrowBody.velocity = new Vector2 (xSpeed, arrowBody.velocity.y); 
    }

    void OnTriggerEnter2D(Collider2D other) 
    {

        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject, 0.2f);
        }

        Destroy(gameObject, 0.01f);
        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject, 0.1f);
        
    }
}

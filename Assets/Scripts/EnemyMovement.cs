using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    Rigidbody2D enemyRigidBody;
    BoxCollider2D enemyTurnBox;
    Transform enemyTransform;
    float enemyLocalScaleX;


    [SerializeField] float enemySpeed = 5f;
    void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyTurnBox = GetComponent<BoxCollider2D>();
        enemyTransform = GetComponent<Transform>();
        
    }
    void Start()
    {
        //enemyLocalScaleX = enemyTransform.localScale.x;
    }

    
    void Update()
    {
        Move();
        
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        //Debug.Log("Turn!!");
        enemySpeed = -enemySpeed;
        enemyTransform.localScale = new Vector2 (Mathf.Sign(enemySpeed), 1f);
        
        
    }
    
    void Move()
    {
        enemyRigidBody.velocity = new Vector2 (enemySpeed, enemyRigidBody.velocity.y);

    }
}

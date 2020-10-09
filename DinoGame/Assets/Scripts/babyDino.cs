using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyDino : MonoBehaviour
{
    public Astronaut astronaut;
    public float speed = 5f;   //move speed of the dino
    Transform target;   //what the dino will be following, in this case our player
    Rigidbody2D rigidBody2D;

    float positioning = 1.5f;

   
    void Start()
    {
        astronaut = astronaut.GetComponent<Astronaut>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update() {
        
    }

    void FixedUpdate()
    {
        if (GlobalVariables.babyDinoAcquired) {
            // acquires target to move towards and then does so at a specified speed
            target = astronaut.transform;
            if (astronaut.direction.x > 0f) {
                positioning = 2.5f;
            }
            else {
                positioning = -1.5f;
            }
            // target.position.y - 1 is so it is at the feet of the astronaut
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(target.position.x + positioning, target.position.y - 1.1f), speed * Time.deltaTime);
        }
    }
}

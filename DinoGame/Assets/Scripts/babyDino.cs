﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyDino : MonoBehaviour
{
    public Astronaut astronaut;
    public float speed = 5f;   //move speed of the dino
    Transform target;   //what the dino will be following, in this case our player
    Rigidbody2D rigidBody2D;

    public GameObject HerbDefeated;
    public GameObject BirdDefeated;
    public GameObject TRexDefeated;

    public GameObject obstacle;

    float positioning = 1.5f;

   
    void Start()
    {
        astronaut.GetComponent<Astronaut>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        if (GlobalVariables.inMainScene && GlobalVariables.HerbDefeated) {
            Debug.Log(HerbDefeated.transform.position);
            transform.position = HerbDefeated.transform.position;
            Debug.Log(rigidBody2D.position);
        }
        if (GlobalVariables.inMainScene && GlobalVariables.BirdDefeated) {
            transform.position = BirdDefeated.transform.position;
        }
        if (GlobalVariables.inMainScene && GlobalVariables.TRexDefeated) {
            transform.position = TRexDefeated.transform.position;
        }
    }

    
    void Update() {
        
    }

    void FixedUpdate()
    {
        if (GlobalVariables.babyDinoAcquired) {
            // acquires target to move towards and then does so at a specified speed
            target = astronaut.transform;
            if (astronaut.direction.x > 0f) {
                positioning = -1.5f;
            }
            else {
                positioning = 2.5f;
            }
            // target.position.y - 1 is so it is at the feet of the astronaut
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(target.position.x + positioning, target.position.y - 1.1f), speed * Time.deltaTime);
            Destroy(obstacle);
        }
    }
}

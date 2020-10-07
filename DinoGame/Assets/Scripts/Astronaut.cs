﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Astronaut : MonoBehaviour
{

    //allows us to change the camera settings later
    public Cinemachine.CinemachineVirtualCamera playerCamera;

    Rigidbody2D rigidBody2D;

    float horizontal; 
    float vertical;

    Vector2 direction;
    Vector2 lastKnownDirection = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //movement
        horizontal = Input.GetAxis("Horizontal");   //value from -1 to 1 that takes input from A and D or leftArrow and rightArrow
        vertical = Input.GetAxis("Vertical");   //value from -1 to 1 that takes input from W and S or upArrow and downArrow

        direction = new Vector2(horizontal, vertical);

        // player is not moving so we need to know the last known direction they were looking
        if (direction == Vector2.zero) {
            direction = lastKnownDirection;
        }
        else {
            // direction is non zero so we can update it to the new last known direction
            lastKnownDirection = direction;
        }

        //checks in the direction the player is looking if there is the babyDino
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit2D hit = Physics2D.Raycast(GetComponent<Rigidbody2D>().position, direction, 4f,  LayerMask.GetMask("BabyDino"));
            if(hit.collider) {
                GlobalVariables.babyDinoAcquired = true;
            }
        }
    }

    void FixedUpdate() {
        // updates the position of the character based off of the values we recieved from Input
        Vector2 position = rigidBody2D.position;
        position.x = position.x + 10f * horizontal * Time.deltaTime;
        position.y = position.y + 10f * vertical * Time.deltaTime;
        rigidBody2D.MovePosition(position);
    }
}
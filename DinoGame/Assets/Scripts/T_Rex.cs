//Code Written by Ricardo Jimenez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Rex : MonoBehaviour
{
  //player object
  GameObject target;
  //Transform object for start position
  Transform initPos;
  //Vector Obbjects for movement
  Vector2 strtDir;//start direction of T-Rex
  Vector2 roamDir;//moving direction of T-Rex
  Rigidbody2D rigidBody2D;
  //T-Rex variables
  private float speed = 11f;//movement speed
  private float initRange = 10f;//range of triggering chase.
  private float chaseRange = 20f;//range T-Rez will chase for.
  private bool chase = false;//determines if T-Rex is chasing player

  void Start(){
    rigidBody2D = GetComponent<Rigidbody2D>();
    target = GameObject.FindGameObjectWithTag("Player");
    initPos.position = Vector2.zero;
  }

  void Update(){
    //if player comes too close to T-Rex, trigger chase(change to fight later)
    if((initPos.position - target.transform.position).magnitude < initRange){
      chase = true;
    }
    else if((initPos.position - target.transform.position).magnitude > chaseRange){
      chase = false;
    }
    //T-Rex has a leashed range; won't chase indefinitely
    if(chase){
      transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    else{//player out of chase range; return to start position
      transform.position = Vector2.MoveTowards(transform.position, initPos.transform.position, speed * Time.deltaTime);
    }
  }
}

//Code Written by Ricardo Jimenez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dino : MonoBehaviour
{
  //player object
  public Astronaut astronaut;
  Transform target;
  //Transform object for start position
  Vector2 start;
  //Vector Obbjects for movement
  Rigidbody2D rigidBody2D;
  //T-Rex variables
  public float speed;//movement speed
  public float initRange;//range of triggering chase.
  public float chaseRange;//range T-Rez will chase for.
  private bool chase = false;//determines if T-Rex is chasing player
  public string species;

  void Start(){
    astronaut = astronaut.GetComponent<Astronaut>();
    rigidBody2D = GetComponent<Rigidbody2D>();
    start = transform.position;

    // if (GlobalVariables.dinoDefeated) {
    //   Destroy(gameObject);
    // }
  }

  void Update(){
    //
  }

  void FixedUpdate(){
    target = astronaut.transform;
    //if player comes too close to T-Rex, trigger chase(change to fight later)
    float a = Vector3.Distance(astronaut.transform.position, start);
    float b = Vector3.Distance(astronaut.transform.position, start);
    if( a < initRange){
      chase = true;
    }
    //T-Rex stops chasing player at a certain range
    if(b > chaseRange){
      chase = false;
    }
    if(chase){
      transform.position = Vector3.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), speed * Time.deltaTime);
    }
    else{
      transform.position = Vector3.MoveTowards(transform.position, start, speed * Time.deltaTime);
    }
  }

  public string getSpecies(){
    return species;
  }

  void OnTriggerStay2D(Collider2D other) {
    Astronaut player = other.GetComponent<Astronaut>();
    if (player) {
      Debug.Log("here");
      GlobalVariables.FightingWith = species;
      SceneManager.LoadScene("Combat");
    }
  }
}

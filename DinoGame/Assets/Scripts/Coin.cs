using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    public AudioSource coinCollec;
    void Start()
    {
        coinCollec = GetComponent<AudioSource>();
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        // checks to see if it collided with the player before adding to the coin count
        Astronaut astronaut = other.GetComponent<Astronaut>();
        if (astronaut) {
            coinCollec.PlayOneShot(coinCollec.clip,1);
            GlobalVariables.coinsCollected++; //updates global coin count
            Destroy(gameObject);    //destroys the coin so it can not be picked up multiple times
        }
    }
}

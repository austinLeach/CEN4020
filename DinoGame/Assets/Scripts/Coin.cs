using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    public AudioSource coinCollec;
    public AudioClip coinss;
    void Start()
    {
        coinCollec = GetComponent<AudioSource>();
        coinss = coinCollec.clip;
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        // checks to see if it collided with the player before adding to the coin count
        Astronaut astronaut = other.GetComponent<Astronaut>();
        if (astronaut) {
            GlobalVariables.coinsCollected++; //updates global coin count
            DestroyObject();
        }
    }
    void DestroyObject()
    {
        AudioSource.PlayClipAtPoint(coinss, transform.position);
        Destroy(gameObject);    //destroys the coin so it can not be picked up multiple times
    }
}

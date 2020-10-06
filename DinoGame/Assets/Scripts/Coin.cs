﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        // checks to see if it collided with the player before adding to the coin count
        Astronaut astronaut = other.GetComponent<Astronaut>();
        if (astronaut) {
            GlobalVariables.coinsCollected++; //updates global coin count
            Debug.Log(GlobalVariables.coinsCollected);
            Destroy(gameObject);    //destroys the coin so it can not be picked up multiple times
        }
    }
}

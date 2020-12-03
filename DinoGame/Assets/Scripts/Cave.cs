using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cave : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Astronaut player = other.GetComponent<Astronaut>();
        if (player) {
            GlobalVariables.inMainScene = false;
           SceneManager.LoadScene("Cave Interior");
        }
    }
}

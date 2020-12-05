using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levers : MonoBehaviour
{
    public GameObject mazeTransition;
    public AudioSource leverSound;
    void Start() {
        mazeTransition.GetComponent<Collider2D>().enabled = false;
        leverSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.leversPressed == 3) {
            mazeTransition.GetComponent<Collider2D>().enabled = true;
            Destroy(gameObject);
            leverSound.Play();
        }
    }
}

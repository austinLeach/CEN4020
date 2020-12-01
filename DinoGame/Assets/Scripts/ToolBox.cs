using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToolBox : MonoBehaviour
{
    Astronaut astronaut;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        astronaut = obj.GetComponent<Astronaut>();
        // if the object that is colliding with the ToolBox is an astronaut
        if(astronaut)
        {
            // Display message on screen to congratulate player for completing the puzzle

            Destroy(gameObject);  // removes the toolbox from the scene to signify that it has been collected already

            SceneManager.LoadScene("end");

        }
    }
}

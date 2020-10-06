using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Astronaut : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCamera;

    Rigidbody2D rigidBody2D;

    float horizontal;
    float vertical;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //movement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        Vector2 position = rigidBody2D.position;
        position.x = position.x + 10f * horizontal * Time.deltaTime;
        position.y = position.y + 10f * vertical * Time.deltaTime;
        rigidBody2D.MovePosition(position);
    }
}

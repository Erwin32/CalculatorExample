using System;
using System.Collections;
using System.Collections.Generic;
using TestProject.Calculator;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float speed = 1;
    
    Vector2 rotation = Vector2.zero;
    public float mouseSpeed = 3;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        
        

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rotation.y += Input.GetAxis ("Mouse X");
            rotation.x += -Input.GetAxis ("Mouse Y");
            transform.eulerAngles = rotation * mouseSpeed;
        }
    }
}

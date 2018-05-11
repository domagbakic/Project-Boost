﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    AudioSource audioSource;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
	}

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }                    
        }
        else
        {
            audioSource.Stop();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.back);
        }
    }    
}

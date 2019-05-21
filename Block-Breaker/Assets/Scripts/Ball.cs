﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Config Parameres")]
    [SerializeField] private PaddleMovement mainPaddle;
    [SerializeField] private float xPush = 2.0f;
    [SerializeField] private float yPush = 15.0f;

    // State
    private Vector2 paddleToBallVector;
    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - mainPaddle.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchBallOnClick();

        }

        

    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(mainPaddle.transform.position.x, mainPaddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchBallOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
           
    }
}
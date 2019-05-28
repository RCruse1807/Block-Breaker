using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Config Parameters
    [Header("Config Parameres")]
    [SerializeField] private PaddleMovement mainPaddle;
    [Header("Ball Physics")]
    [SerializeField] private float xPush = 2.0f;
    [SerializeField] private float yPush = 15.0f;
    [SerializeField] private float randomFactor = 0.2f;
    [Header("Ball Audio")]
    [SerializeField] private AudioClip[] ballAudioSounds;

    //State
    private Vector2 paddleToBallVector;
    private bool hasStarted = false;

    //Cached reference
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - mainPaddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();

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
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballAudioSounds[Random.Range(0, ballAudioSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;

        }

    }
}

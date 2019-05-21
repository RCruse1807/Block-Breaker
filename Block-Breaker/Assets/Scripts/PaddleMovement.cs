using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [Header("Screen Config Parameters")]
    [SerializeField] private float screenWidthInUnits = 16.0f;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        paddlePos.x = Mathf.Clamp(mousePosInUnits, xMin, xMax);
        transform.position = paddlePos;
    }
}

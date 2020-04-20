using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //config parameters
    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float platformMixX = 5f;
    [SerializeField] float platformMaxX = 16f;


    Ball ball = null;
    State state = null;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        state = FindObjectOfType<State>();
    }

    // Update is called once per frame
    void Update()
    {
        var paddlePosition = this.transform.position;
        paddlePosition.x = Mathf.Clamp(GetXPos(), platformMixX, platformMaxX);
        this.transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        float pos = 0f;

        if(state.IsAutoPlayEnabled())
        {
            pos = ball.transform.position.x;
        }
        else
        {
            pos = (Input.mousePosition.x / Screen.width) * screenWidthUnits;
        }

        return pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //config parameters
    [SerializeField] float screenWidthUnits = 16f;
    [SerializeField] float platformMixX = 5f;
    [SerializeField] float platformMaxX = 16f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newX = (Input.mousePosition.x / Screen.width) * screenWidthUnits;
        var paddlePosition = this.transform.position;
        paddlePosition.x = Mathf.Clamp(newX, platformMixX, platformMaxX);
        this.transform.position = paddlePosition;
    }
}

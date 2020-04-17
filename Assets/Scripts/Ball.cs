using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //config parameters
    [SerializeField] Paddle paddle = null;
    [SerializeField] float launchX = 0;
    [SerializeField] float launchY = 0;
    [SerializeField] AudioClip[] sounds = null;
    [SerializeField] float randomPush = .2f;

    //cache
    Rigidbody2D ballRigidBody = null;

    //state
    Vector2 paddleToBallDistance;
    bool gameStart = true;
    AudioSource ballAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallDistance = this.transform.position - paddle.transform.position;
        ballAudioSource = this.GetComponent<AudioSource>();
        ballRigidBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart)
        {
            LockBallToPaddle();
        }

        if(Input.GetMouseButtonDown(0) && gameStart)
        {
            gameStart = !gameStart;
            LaunchOnClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = paddle.transform.position;
        this.transform.position = (paddlePosition + paddleToBallDistance);
    }

    private void LaunchOnClick()
    {
        ballRigidBody.velocity = new Vector2(
            Random.Range(-launchX, launchX), Random.Range(launchY, 2*launchY));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.name.Contains("Block"))
        {
            AudioClip clip = sounds[Random.Range(0, sounds.Length)];
            ballAudioSource.PlayOneShot(clip);
            //this.gameObject.GetComponent<AudioSource>().Play();
        }
        Vector2 speed = ballRigidBody.velocity;

        int xDirection = speed.x > 0 ? 1 : -1;
        int yDirection = speed.y > 0 ? 1 : -1;

        float newX = speed.x + (Random.Range(randomPush, 2 * randomPush) * xDirection);
        float newY = speed.y + (Random.Range(randomPush, 2 * randomPush) * yDirection);

        ballRigidBody.velocity = new Vector2(newX, newY);
    }
}

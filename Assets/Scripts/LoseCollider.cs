using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    State state;

    private void Start()
    {
        state = FindObjectOfType<State>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("You lose.  Collided with " + collision.name);
        state.ResetGame();
        //SceneManager.LoadScene("Game Over");
    }
}

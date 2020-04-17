using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{

    [SerializeField] AudioClip[] songList = null;

    AudioSource player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
        player.clip = songList[0];
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Soundtrack : MonoBehaviour
{

    [SerializeField] AudioClip[] songList = null;
    [SerializeField] AudioClip successStinger = null;
    [SerializeField] AudioClip failureStinger = null;

    AudioSource player = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayLevelTrack(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void PlayLevelTrack(int index)
    {
        index = index < 0 ? 0 : index;

        player = GetComponent<AudioSource>();
        if(index < songList.Length)
            player.clip = songList[index];
        player.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopBackgroundMusic()
    {
        player.Stop();
    }

    public void PlaySuccessStinger()
    {
        StopBackgroundMusic();
        player.clip = successStinger;
        player.Play();
    }

    public void PlayFailureStinger()
    {
        StopBackgroundMusic();
        player.clip = failureStinger;
        player.Play();
    }
}

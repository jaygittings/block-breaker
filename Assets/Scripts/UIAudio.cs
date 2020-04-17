using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] GameObject mouseOver = null;
    [SerializeField] GameObject mouseClick = null;

    AudioSource mouseOverAudio = null;
    AudioSource mouseClickAudio = null;

    // Start is called before the first frame update
    void Start()
    {
        if (mouseOver != null)
            mouseOverAudio = mouseOver.GetComponent<AudioSource>();
        if (mouseClick != null)
            mouseClickAudio = mouseClick.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOnMouseClick()
    {
        mouseClickAudio.PlayOneShot(mouseClickAudio.clip);
    }

    public void PlayOnMouseOver()
    {
        mouseOverAudio.PlayOneShot(mouseOverAudio.clip);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound = null;
    [SerializeField] GameObject particleEffect = null;
    [SerializeField] Sprite[] hitSprites = null;
    //[SerializeField] GameObject playspace = null;

    //cache references
    State state = null;
    //GameObject background = null;
    //SpriteRenderer backgroundRenderer = null;
    //Color backgroundColor = new Color();

    //state variables
    int currentHits = 0;

    private void Start()
    {
        state = GameObject.FindObjectOfType<State>();
        if(state != null)
        {
            if(gameObject.tag == "Breakable")
            {
                state.AddBlock();
            }
        }

        

        //if(playspace != null)
        //{
        //    backgroundRenderer = playspace.GetComponentInChildren<SpriteRenderer>();
        //    backgroundColor = backgroundRenderer.color;
        //    //Debug.Log(backgroundRenderer.gameObject.name);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        int maxHits = hitSprites.Length;
        if(gameObject.tag == "Breakable")
        {
            currentHits++;
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
            //if(backgroundRenderer != null)
            //{
            //    backgroundColor = backgroundRenderer.color;
            //    Debug.Log(backgroundRenderer.color);
            //    backgroundColor = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, backgroundColor.a + 10f);
            //    backgroundRenderer.color = backgroundColor;
            //    Debug.Log(backgroundRenderer.color);
            //}
            if(currentHits >= maxHits)
            {
                TriggerParticleEffect();
                if (state != null)
                    state.RemoveBlock();
                Destroy(this.gameObject, 0.1f);
            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        var spriteIndex = 0;
        if(hitSprites != null && currentHits >= hitSprites.Length)
        {
            spriteIndex = hitSprites.Length - 1;
        }
        else
        {
            spriteIndex = currentHits;
        }
        this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void TriggerParticleEffect()
    {
        var obj = Instantiate(particleEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(obj, 2f);
    }


}

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

    //cache references
    State state = null;

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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        int maxHits = hitSprites.Length;
        if(gameObject.tag == "Breakable")
        {
            currentHits++;
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
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

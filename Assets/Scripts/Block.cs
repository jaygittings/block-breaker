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
    [SerializeField] Color[] colors = null;
    [SerializeField] float colorChangeRate = 1.0f;
    [SerializeField] bool changeColor = false;
    [SerializeField] bool isRandom = false;
    //[SerializeField] GameObject playspace = null;

    //cache references
    State state = null;
    bool reportedHit = false;
    int currentColor = 0;
    SpriteRenderer sRender = null;
    ParticleSystem particle = null;
    ParticleSystem.MainModule main = new ParticleSystem.MainModule();

    //GameObject background = null;
    //SpriteRenderer backgroundRenderer = null;
    //Color backgroundColor = new Color();

    //state variables
    int currentHits = 0;
    float nextTime = 0f;
    float totalTime = 0f;

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

        sRender = GetComponent<SpriteRenderer>();
        particle = particleEffect.GetComponent<ParticleSystem>();
        main = particle.main;
        nextTime = colorChangeRate;
        //Debug.Log(nextTime);

        

        //if(playspace != null)
        //{
        //    backgroundRenderer = playspace.GetComponentInChildren<SpriteRenderer>();
        //    backgroundColor = backgroundRenderer.color;
        //    //Debug.Log(backgroundRenderer.gameObject.name);
        //}
    }

    private void Update()
    {
        totalTime += Time.deltaTime;

        if (changeColor)
        {
            if(totalTime >= nextTime)
            {

                if(isRandom)
                {
                    sRender.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1.0f);
                }
                else
                {
                    sRender.color = new Color(colors[currentColor].r, colors[currentColor].g, colors[currentColor].b);
                    currentColor = (currentColor + 1 >= colors.Length) ? 0 : currentColor + 1;
                }
                nextTime = totalTime + colorChangeRate;
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
                if (state != null && !reportedHit)
                {
                    state.RemoveBlock(maxHits);
                    reportedHit = true;
                }
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
        sRender.sprite = hitSprites[spriteIndex];
    }

    private void TriggerParticleEffect()
    {
        main.startColor = sRender.color;
        var obj = Instantiate(particleEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);
        Destroy(obj, 2f);
    }


}

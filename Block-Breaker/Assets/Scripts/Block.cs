using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Config Parameters
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private GameObject blockVFX;
    [SerializeField] private Sprite[] hitSprites;

    // Cached Reference
    Level level;
    GameSession gameStatus;

    // State Variables 
    [SerializeField] private int timesHit; // Used for debuging purposes

    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
        
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        PlayBlockSound();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerBlockVFX();
    }

    private void PlayBlockSound()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerBlockVFX()
    {
        Instantiate(blockVFX, transform.position, transform.rotation);
        Destroy(blockVFX, 2f);
    }
}

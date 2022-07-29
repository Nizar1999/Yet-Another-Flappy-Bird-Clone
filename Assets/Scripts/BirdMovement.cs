using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] int jumpHeight = 250;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip dieSound;

    private Rigidbody2D bird;

    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        bird = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if(state == GameState.Idle)
        {
            bird.velocity = Vector2.zero;
            bird.angularVelocity = 0.0f;
            bird.rotation = 0;
            transform.position = Vector2.zero;
            InvokeRepeating("Jump", 0f, 1.0f);
        }
            
        if (state == GameState.Play)
            CancelInvoke("Jump");
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (GameManager.instance.state == GameState.Play)
        {
            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        bird.velocity = Vector2.zero;
        bird.AddForce(new Vector2(0, jumpHeight));
        AudioManager.instance.playSound(jumpSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.state == GameState.Play)
        {
            bird.velocity = new Vector2(0, -1);
            AudioManager.instance.playSound(dieSound, 0.3f);
            GameManager.instance.UpdateGameState(GameState.Lose);
        }
    }
}
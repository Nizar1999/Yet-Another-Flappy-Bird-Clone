using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    [SerializeField] float pipeSpeed;
    private bool passed = false;
    private Rigidbody2D pipes;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        pipes = GetComponent<Rigidbody2D>();
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    void Start()
    {
        pipes.AddForce(new Vector2(pipeSpeed, 0));
    }
    void Update()
    {
        if (GameManager.instance.state != GameState.Play)
        {
            pipes.velocity = new Vector2(0,0);
            return;
        }
        RemoveIfOutOfBounds();
    }

    void RemoveIfOutOfBounds()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.0)
            Destroy(gameObject);
        else
            CheckIfPassed();
    }

    void CheckIfPassed()
    {
        if (passed)
            return;
        if (transform.position.x < -1.7)
        {
            passed = true;
            GameManager.instance.UpdateScore();
        }
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Idle)
        {
            Destroy(gameObject);
        }
    }
}

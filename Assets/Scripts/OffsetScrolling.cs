using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    public float ScrollSpeed = 0.5f;
    private Vector2 savedOffset;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        savedOffset = renderer.material.mainTextureOffset;
    }

    private void Update()
    {
        if (GameManager.instance.state != GameState.Play)
            return;
        float x = Mathf.Repeat(Time.time * ScrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        renderer.material.mainTextureOffset = offset;
    }

    private void OnDisable()
    {
        renderer.material.mainTextureOffset = savedOffset;
    }
}

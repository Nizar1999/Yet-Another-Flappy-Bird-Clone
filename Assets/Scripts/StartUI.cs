using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    private Animator pressToStartAnim;
    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        pressToStartAnim = GetComponent<Animator>();
    }
    void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        pressToStartAnim.ResetTrigger("Idle");
        if (state == GameState.Play)
        {
            pressToStartAnim.SetTrigger("Play");
        }
        if(state == GameState.Idle)
        {
            pressToStartAnim.SetTrigger("Idle");
        }
    }

}

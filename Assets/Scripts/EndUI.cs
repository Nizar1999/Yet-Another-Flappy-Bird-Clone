using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    [SerializeField] Text score;
    private Animator panelAnim;
    
    void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        panelAnim = GetComponent<Animator>();
    }

    private void OnGameStateChanged(GameState state)
    {
        panelAnim.ResetTrigger("Idle");
        if (state == GameState.Lose)
        {
            score.text = "Score: " + GameManager.instance.score.ToString();
            panelAnim.SetTrigger("Lose");
        }
        if (state == GameState.Idle)
        {
            panelAnim.SetTrigger("Idle");
        }
    }
}

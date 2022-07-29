using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] AudioClip updateScoreSound;
    private Text score;

    void Awake()
    {
        score = GetComponent<Text>();
        GameManager.OnScoreChanged += OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        score.text = GameManager.instance.score.ToString();
        AudioManager.instance.playSound(updateScoreSound);
    }
}

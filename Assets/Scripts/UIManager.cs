using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI scoreText;
    public int score;
    public void Start()
    {
        EventManager.Instance.RegisterListener<OnEnemyDie>(ChangeScore);
        EventManager.Instance.RegisterListener<OnPlayerHurt>(UpdateHealthBar);
    }

    public void ChangeScore(OnEnemyDie ed)
    {
        score += ed.val;

        scoreText.text = score.ToString();
    }


    public void UpdateHealthBar(OnPlayerHurt ph)
    {
        
        healthBar.fillAmount = ph.newHealth / 10f;
        
    }
    
    
    
}

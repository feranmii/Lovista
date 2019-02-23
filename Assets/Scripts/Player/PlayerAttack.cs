using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerManager _playerManager;

    public GameObject attackHitBox;
    
    private void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Attack
            Attack();
        }
        else
        {
            attackHitBox.SetActive(false);
        }
        
    }

    public void Attack()
    {
        attackHitBox.SetActive(true);
    }
}


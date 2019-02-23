using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerManager : MonoBehaviour, IDammageable
{

    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                return _instance;
            }

            return _instance;
        }
        
    }

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;

    public ParticleSystem footstepParticles;
    public ParticleSystem jumpParticles;
    
    public int health = 10;
    
    [HideInInspector]
    public Animator anim;

    public void Awake()
    {
        _instance = this;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        EventManager.Instance.RegisterListener<OnDialogueStart>(DisablePlayer);
        EventManager.Instance.RegisterListener<OnDialogueEnd>(EnablePlayer);
        
    }

    public void Damage(int amount, int launchDirection)
    {
        
        playerMovement.KnockBack(launchDirection);
        health -= amount;
        
        OnPlayerHurt pd = new OnPlayerHurt
        {
            newHealth = health
        };
        
        EventManager.Instance.FireEvent(pd);
        //Do Death Logic
    }

    public void DisablePlayer(EventInfo info)
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
    }

    public void EnablePlayer(EventInfo info)
    {
        playerMovement.enabled = true;
        playerAttack.enabled = true;
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitBox : MonoBehaviour
{

    private Transform parent;

    private int launchDirection = 1;
    private void Start()
    {
        parent = gameObject.transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        var target = other.gameObject.GetComponent<IDammageable>();
        
        if (target != null)
        {
            if (parent.position.x < other.transform.position.x)
            {
                launchDirection = 1;
                
            }
            else
            {
                launchDirection = -1;
            }
            
            target.Damage(1, launchDirection);
        }
    }
}

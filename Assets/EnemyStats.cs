using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int maxHealth = 3;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

}

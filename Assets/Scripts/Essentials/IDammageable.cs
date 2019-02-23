using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDammageable
{
    void Damage(int amount, int launchDirection);
}

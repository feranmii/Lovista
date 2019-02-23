using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
 
    
    public void EmitFootstepsParticles()
    {
        PlayerManager.Instance.footstepParticles.Emit(5);
    }
    
    public void EmitJumpParticles()
    {
               
    }
    
    
    
    
    
    
}

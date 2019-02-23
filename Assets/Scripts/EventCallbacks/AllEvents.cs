using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using UnityEngine;

namespace EventCallbacks
{
    
    public class OnEnemyDie : EventInfo
    {
        public int val;
        public Enemy en;
    }

    public class OnPlayerHurt : EventInfo
    {
        public int newHealth;
    }

    public class OnDialogueStart : EventInfo
    {
        
    }

    public class OnDialogueEnd : EventInfo
    {
        
    }

    public class OnLevelBegin : EventInfo
    {
        
    }

    public class OnLevelEnd : EventInfo
    {
        
    }
    
}

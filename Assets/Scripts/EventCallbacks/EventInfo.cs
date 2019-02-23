using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventCallbacks
{

    public abstract class EventInfo
    {
        /*
         * Base Event Info
         * could be a struct, readonly etc.
         */

        public string EventDescription;
    }

    public class DebugEventInfo : EventInfo
    {
        public int VerbosityLevel;
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using Malee;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Create New Dialogue")]
public class Dialogue : ScriptableObject
{
    
    public string name;
    public bool isFinished;
    [Reorderable(paginate = false, singleLine = false, sortable = false)]
    public SentencesList sentences;
    public Dialogue NextDialogue;
    
    
    [System.Serializable]
    public struct Sentence {

        [TextArea(3,10)]
        public string sentence;
       
    }

    [System.Serializable]
    public class SentencesList : ReorderableArray<Sentence> {
    }
    
}
    

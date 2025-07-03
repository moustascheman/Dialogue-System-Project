using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class DialogueNode : ScriptableObject
{

    [SerializeField]
    public String NodeId;


    //public List<NodeTags> attachedTags;

    private string guid = Guid.NewGuid().ToString();



    

    public abstract DialogueNode GetNextNode(int choice);

    public abstract String GetResponseText();

    public abstract List<DialogueChoice> GetChoices();

    public virtual List<DialogueChoice> GetValidChoices()
    {
        return GetChoices();
    }

    public virtual List<DialogueChoice> GetNonHiddenChoices()
    {
        return GetChoices();
    }

}

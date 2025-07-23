using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class DialogueNode
{

    [SerializeField]
    public string NodeId => guid;


    //public List<NodeTags> attachedTags;

    private string guid = Guid.NewGuid().ToString();
    public string EditorId = "New Node";

    public Vector2 pos;



    

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

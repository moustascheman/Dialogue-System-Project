using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseNode : DialogueNode
{
    [SerializeField]
    public string textValue;

    [SerializeReference]
    public DialogueNode nextNode;


    public override DialogueNode GetNextNode(int choice)
    {
        return nextNode;
    }

    public override string GetResponseText()
    {
        return textValue;
    }

    public override List<DialogueChoice> GetChoices()
    {
        return null;
    }

    
}

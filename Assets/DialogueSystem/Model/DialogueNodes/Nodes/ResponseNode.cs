using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseNode : DialogueNode
{
    [SerializeField]
    private string textValue;

    [SerializeReference]
    private DialogueNode nextNode;


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

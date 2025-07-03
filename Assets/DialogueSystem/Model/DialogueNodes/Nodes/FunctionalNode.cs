using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FunctionalNode : DialogueNode
{
    [SerializeField]
    private DialogueNode nextNode;
    UnityEvent nodeFunction;
    public override List<DialogueChoice> GetChoices()
    {
        return null;
    }

    public override DialogueNode GetNextNode(int choice)
    {
        // Do function here!
        nodeFunction.Invoke();
        return nextNode;
    }

    public override string GetResponseText()
    {
        return null;
    }
    
}

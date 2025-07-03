using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// TODO: Allow User to evaluate multiple conditions at once (a condition block) and return the node for the first matching condition

public class ConditionalNode : DialogueNode
{
    [SerializeField]
    private AbstractCondition condition;

    [SerializeField]
    private DialogueNode trueNode;

    [SerializeField]
    private DialogueNode falseNode;
    public override List<DialogueChoice> GetChoices()
    {
        return null;
    }

    public override DialogueNode GetNextNode(int choice)
    {
        if (condition.EvaluateCondition())
        {
            return trueNode;
        }
        else
        {
            return falseNode;
        }
    }

    public override string GetResponseText()
    {
        return null;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DialogueNode("Choice Node")]
[Serializable]
public class ChoiceNode : DialogueNode
{

    
    public List<DialogueChoice> choices;


    public override DialogueNode GetNextNode(int choice)
    {
        return choices[choice].GetNextNode();
    }

    public override List<DialogueChoice> GetChoices()
    {
        return choices;
    }

    public override List<DialogueChoice> GetValidChoices()
    {
        return choices.Where(choice => choice.isValid()).ToList();
    }

    public override List<DialogueChoice> GetNonHiddenChoices()
    {
        return choices.Where(choice => !choice.hidden).ToList();
    }

    public override string GetResponseText()
    {
        return null;
    }

    
}

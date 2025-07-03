using System.Collections.Generic;
using UnityEngine;


public class HybridNode : DialogueNode
{

    [SerializeField]
    private string ResponseText;
    [SerializeField]
    private List<DialogueChoice> choices;


    public override DialogueNode GetNextNode(int choice)
    {
        return choices[choice].GetNextNode();
    }

    public override string GetResponseText()
    {
        return ResponseText;
    }

    public override List<DialogueChoice> GetChoices()
    {
        return choices;
    }
    
}

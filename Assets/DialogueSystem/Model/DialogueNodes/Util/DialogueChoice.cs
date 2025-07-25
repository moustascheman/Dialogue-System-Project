using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeReference]
    public DialogueNode nextNode;
    public bool hidden = false;

    private string DialogueChoiceId;

    public string ChoiceText = "PLACEHOLDER";




    [SerializeReference]
    private ConditionalList conditionalList;

    public DialogueChoice()
    {
        ChoiceText = "PLACEHOLDER";
    }    




    public DialogueNode GetNextNode()
    {
        return nextNode;
    }

    public string ChoiceId { get => ChoiceId; }


    public bool isValid()
    {
        return true;
    }

    
    
}

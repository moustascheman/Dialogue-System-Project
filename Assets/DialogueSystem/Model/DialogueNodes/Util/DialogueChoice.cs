using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    [SerializeReference]
    private DialogueNode nextNode;
    public bool hidden = false;

    private string DialogueChoiceId;

    public string ChoiceText = "PLACEHOLDER";

    [SerializeField]
    private ConditionalList conditionalList;

    


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

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resp Node", menuName = "Scriptable Objects/DialogueSystem/Debug/ResponseNode")]
public class ResponseNode : DialogueNode
{
    [SerializeField]
    private string textValue;

    [SerializeField]
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

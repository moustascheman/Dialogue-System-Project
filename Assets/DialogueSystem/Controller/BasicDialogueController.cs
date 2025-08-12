using UnityEngine;

public class BasicDialogueController
{
    private Conversation m_conversation;

    private DialogueNode m_currentNode;

    public DialogueNode currentNode => m_currentNode;


    public BasicDialogueController(Conversation convo)
    {
        m_conversation = convo;
        m_currentNode = convo.GetEntryNode();
    }

    public DialogueNode GetNextNode(int choice)
    {
        DialogueNode next = currentNode.GetNextNode(choice);
        m_currentNode = next;
        return currentNode;
    }
}

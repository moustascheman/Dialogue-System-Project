using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptable Objects/Conversation")]
public class Conversation : ScriptableObject
{
    public string ConversationName;



    private string guid = Guid.NewGuid().ToString();

    public string id => guid;


    [SerializeReference]
    public List<DialogueNode> nodes = new List<DialogueNode>();

    [SerializeReference]
    public List<DialogueNode> entryNodes = new List<DialogueNode>();


    public void AddDialogueNode(Type t)
    {

    }


    public bool IsStartNode(DialogueNode node)
    {
        return entryNodes.Contains(node);
    }

    //Need to change this later to use a container object so I can add conditions
    public void AddStartNode(DialogueNode node)
    {
        entryNodes.Add(node);
    }

    public void RemoveStartNode(DialogueNode node)
    {
        entryNodes.Remove(node);
    }

    public void AddDialogueNode(DialogueNode node)
    {
        nodes.Add(node);
    }


    public DialogueNode GetEntryNode()
    {
        if (entryNodes.Count != 0)
        {
            return entryNodes.First();
        }
        else
        {
            return nodes.First();
        }
    }
    
}

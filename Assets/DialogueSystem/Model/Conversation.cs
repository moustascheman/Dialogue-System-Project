using System;
using System.Collections;
using System.Collections.Generic;
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

    public List<DialogueNode> entryNodes;


    public void AddDialogueNode(Type t)
    {
        
    }


    public void AddDialogueNode(DialogueNode node)
    {
        nodes.Add(node);
    }


    
}

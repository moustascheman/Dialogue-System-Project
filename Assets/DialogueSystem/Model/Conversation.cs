using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptable Objects/Conversation")]
public class Conversation : ScriptableObject
{
    public string ConversationName;


    [SerializeField]
    private string guid = Guid.NewGuid().ToString();

    [SerializeField]
    private List<DialogueNode> nodes;

    public List<DialogueNode> entryNodes;


    
}

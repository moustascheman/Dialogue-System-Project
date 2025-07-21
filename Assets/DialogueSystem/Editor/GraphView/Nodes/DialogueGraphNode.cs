using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

public class DialogueGraphNode : Node
{

    private DialogueNode m_Node;

    public DialogueNode node => m_Node;

    public string id => m_Node.NodeId;

    public DialogueGraphNode(DialogueNode node)
    {
        this.AddToClassList("dialogue-graph-node");
        m_Node = node;
    }
    
}

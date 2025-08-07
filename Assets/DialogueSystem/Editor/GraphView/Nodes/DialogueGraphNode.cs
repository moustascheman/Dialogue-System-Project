using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System;

public abstract class DialogueGraphNode : Node
{
    public delegate void ChangeStartStatus(DialogueNode node);

    public event ChangeStartStatus StartingNodeStatusChangeEvent;

    private DialogueNode m_Node;

    public DialogueNode node => m_Node;

    public Port inputPort;

    public string id => m_Node.NodeId;

    public List<Edge> edgeList = new List<Edge>();

    public event EventHandler ValueChangedEvent;


    public DialogueGraphNode(DialogueNode node, bool startingNode)
    {
        this.AddToClassList("dialogue-graph-node");
        m_Node = node;
    }


    public abstract void ConnectNodeWithPort(Port ouput, Port input);

    protected virtual void OnValueChange()
    {
        ValueChangedEvent?.Invoke(this, null);
    }

    protected virtual void OnStartingNodeStatusChange()
    {
        StartingNodeStatusChangeEvent?.Invoke(node);
    }

    public abstract List<Edge> DrawEdges(Dictionary<string, DialogueGraphNode> nodeDict);

    public abstract void DisconnectPort(Port output);

    public abstract void RemoveIncomingConnections(Port output);
}

using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

public abstract class DialogueGraphNode : Node
{

    private DialogueNode m_Node;

    public DialogueNode node => m_Node;

    public Port inputPort;

    public string id => m_Node.NodeId;

    public List<Edge> edgeList = new List<Edge>();

    public DialogueGraphNode(DialogueNode node)
    {
        this.AddToClassList("dialogue-graph-node");
        m_Node = node;
    }


    public abstract void ConnectNodeWithPort(Port ouput, Port input);

    public abstract List<Edge> DrawEdges(Dictionary<string, DialogueGraphNode> nodeDict);

    public abstract void DisconnectPort(Port output);

    public abstract void RemoveIncomingConnections(Port output);
}

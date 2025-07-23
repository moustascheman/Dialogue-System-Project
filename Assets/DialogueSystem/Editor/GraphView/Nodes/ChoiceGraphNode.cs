using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChoiceGraphNode : DialogueGraphNode
{
    public ChoiceGraphNode(ChoiceNode node) : base(node)
    {
        
    }

    public override void ConnectNodeWithPort(Port ouput, Port input)
    {
        throw new System.NotImplementedException();
    }

    public override void DisconnectPort(Port output)
    {
        throw new System.NotImplementedException();
    }

    public override List<Edge> DrawEdges(Dictionary<string, DialogueGraphNode> nodeDict)
    {
        return new List<Edge>();
    }
}

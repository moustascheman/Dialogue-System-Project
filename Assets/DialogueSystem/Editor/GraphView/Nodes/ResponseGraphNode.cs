using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ResponseGraphNode : DialogueGraphNode
{
    public ResponseGraphNode(ResponseNode node) : base(node)
    {
        this.title = "Response Node";
        this.AddToClassList("Response-Node");
        Port input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Multi, typeof(Node));
        Port output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(Node));
        this.inputContainer.Add(input);
        this.outputContainer.Add(output);
    }


    
}

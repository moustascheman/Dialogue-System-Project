using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Messages;
using Unity.Properties;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ResponseGraphNode : DialogueGraphNode
{

    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset;

    private Port outputPort;
    public ResponseGraphNode(ResponseNode node, bool startingNode) : base(node, startingNode)
    {
        //m_VisualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DialogueSystem/Editor/GraphView/UXML/ResponseNode.uxml");
        //VisualElement root = this;
        //m_VisualTreeAsset.CloneTree(this);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem/Editor/GraphView/USS/ResponseNode.uss");
        styleSheets.Add(styleSheet);

        AddToClassList("response-node");

        this.title = node.EditorId;



        ResponseNode thisNode = node as ResponseNode;
        Toggle isStartNode = new Toggle("Start Node?");
        isStartNode.value = startingNode;
        isStartNode.RegisterValueChangedCallback(e =>
        {
            OnStartingNodeStatusChange();
        });
        TextField textField = new TextField("Response Text:");
        textField.value = thisNode.textValue;
        textField.RegisterValueChangedCallback(e =>
        {
            thisNode.textValue = e.newValue;
            OnValueChange();
        });

        TextField editorIdField = new TextField("Editor ID:");
        editorIdField.value = thisNode.EditorId;
        editorIdField.RegisterValueChangedCallback(e =>
        {
            this.title = e.newValue;
            thisNode.EditorId = e.newValue;
            OnValueChange();
        });

        textField.multiline = true;
        textField.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
        textField.AddToClassList("ResponseText");
        VisualElement contentBox = new VisualElement();
        contentBox.name = "ContentBox";
        contentBox.Add(isStartNode);
        contentBox.Add(editorIdField);
        contentBox.Add(textField);

        extensionContainer.Add(contentBox);



        contentBox.AddToClassList("content-box");



        Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(DialogueGraphNode));
        inputPort = input;
        outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(DialogueGraphNode));
        this.inputContainer.Add(input);
        //this.outputContainer.Add(output);
        contentBox.Add(outputPort);
        RefreshExpandedState();

    }

    public override void ConnectNodeWithPort(Port output, Port input)
    {
        DialogueGraphNode nextGraphNode = input.node as DialogueGraphNode;
        ResponseNode thisNode = node as ResponseNode;
        thisNode.nextNode = nextGraphNode.node;
        Debug.Log(thisNode.nextNode.EditorId);
    }

    public override void DisconnectPort(Port output)
    {
        ResponseNode resp = node as ResponseNode;
        resp.nextNode = null;
        Debug.Log($"Disconnected connect node for {resp.EditorId}. New output is now {resp.nextNode}");
    }

    public override List<Edge> DrawEdges(Dictionary<string, DialogueGraphNode> nodeDict)
    {
       
        ResponseNode thisNode = node as ResponseNode;
        if (thisNode.nextNode != null)
        {
            string nextNodeID = thisNode.nextNode.NodeId;
            DialogueGraphNode nextGraphNode = nodeDict[nextNodeID];
            Debug.Log($"Connecting Node {thisNode.EditorId} to Node {nextGraphNode.node.EditorId}");
            var edge = outputPort.ConnectTo(nextGraphNode.inputPort);
            edgeList.Add(edge);

        }
        return edgeList;
    }

    public override void RemoveIncomingConnections(Port output)
    {
        ResponseNode rNode = node as ResponseNode;
        rNode.nextNode = null;
    }
}

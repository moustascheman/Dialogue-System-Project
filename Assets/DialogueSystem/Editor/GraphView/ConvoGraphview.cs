using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEditor.Graphs;
using System.Collections.Generic;
using System;
using NUnit.Framework.Interfaces;
using System.Runtime.Remoting;
using System.Numerics;
using System.Linq;


[UxmlElement]
public partial class ConvoGraphview : GraphView
{
    public Conversation convo;
    public SerializedObject obj;

    public ConversationEditorWindow window;

    public List<DialogueGraphNode> nodeList = new List<DialogueGraphNode>();
    public Dictionary<String, DialogueGraphNode> nodeDict = new Dictionary<string, DialogueGraphNode>();

    private ConversationGraphSearchProvider searchProvider;
    public ConvoGraphview()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        Blackboard bb = new Blackboard(this);
        Add(bb);

        searchProvider = ConversationGraphSearchProvider.CreateInstance<ConversationGraphSearchProvider>();
        searchProvider.graphview = this;
        this.nodeCreationRequest = ShowSearchWindow;

        StyleSheet styles = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem/Editor/GraphView/USS/GridStyleSheet.uss");
        styleSheets.Add(styles);
        graphViewChanged += OnGraphViewChangedEvent;

        //Undo.undoRedoPerformed += RenderExistingNodes;
    }


    //TODO: Fix this issue with undos not removing editor nodes 
    private GraphViewChange OnGraphViewChangedEvent(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            Undo.RecordObject(obj.targetObject, "Removed Node");
            graphViewChange.elementsToRemove.OfType<DialogueGraphNode>().ToList()
            .ForEach(elem =>
            {
                Debug.Log("NODE REMOVED");
                convo.nodes.Remove(elem.node);
                nodeList.Remove(elem);
                nodeDict.Remove(elem.id);
                RemoveElement(elem);
                obj.Update();
            });
            graphViewChange.elementsToRemove.OfType<UnityEditor.Experimental.GraphView.Edge>().ToList().ForEach(edge =>
            {
                DialogueGraphNode dNode = edge.output.node as DialogueGraphNode;
                dNode.DisconnectPort(edge.output);
            });
            //RenderExistingNodes();
        }
        if (graphViewChange.edgesToCreate != null)
        {
            foreach (UnityEditor.Experimental.GraphView.Edge edge in graphViewChange.edgesToCreate)
            {
                Port inputPort = edge.input;
                Port outputPort = edge.output;
                DialogueGraphNode outputNode = outputPort.node as DialogueGraphNode;
                outputNode.ConnectNodeWithPort(outputPort, inputPort);
            }
        }
        return graphViewChange;
    }

    private void ShowSearchWindow(NodeCreationContext context)
    {
        searchProvider.target = (VisualElement)focusController.focusedElement;
        SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchProvider);
    }

    public void AddNode(NodeInfoContainer cont, UnityEngine.Vector2 mousePos)
    {
        DialogueNode node = (DialogueNode)Activator.CreateInstance(cont.nodeType);
        Undo.RecordObject(obj.targetObject, "Added Node");

        //Dislike this. Need to figure out a better way to have graph position of nodes persist
        node.pos = mousePos;
        convo.AddDialogueNode(node);
        obj.Update();
        AddNodetoGraph(cont, node);


    }


    private void AddNodetoGraph(NodeInfoContainer cont, DialogueNode node)
    {
        string typename = cont.title;

        DialogueGraphNode d_node = null;

        if (typename == "Response Node")
        {
            d_node = new ResponseGraphNode(node as ResponseNode);

        }
        else if (typename == "Choice Node")
        {
            d_node = new ChoiceGraphNode(node as ChoiceNode);
        }
        d_node.SetPosition(new Rect(node.pos, new UnityEngine.Vector2()));

        if (!nodeDict.ContainsKey(d_node.id))
        {
            AddElement(d_node);
            nodeList.Add(d_node);
            nodeDict.Add(d_node.id, d_node);
        }
    }

    public void RenderExistingNodes()
    {
        //this.Clear();
        // DeleteElements(graphElements);
        //  nodeDict.Clear();
        //  nodeList.Clear();
        foreach (DialogueNode node in convo.nodes)
        {
            Type t = node.GetType();
            NodeInfoContainer cont = getContainerForType(t);
            AddNodetoGraph(cont, node);
        }
        //Generate edges

        foreach (DialogueGraphNode gNode in nodeList)
        {
            GenerateEdges(gNode);
        }
    }


    private void GenerateEdges(DialogueGraphNode graphNode)
    {
        foreach (var edge in graphNode.DrawEdges(nodeDict))
        {
            this.Add(edge);
        }
    }

    private NodeInfoContainer getContainerForType(Type t)
    {
        string title = "";
        if (t == typeof(ResponseNode))
        {
            title = "Response Node";
        }
        else if (t == typeof(ChoiceNode))
        {
            title = "Choice Node";
        }
        NodeInfoContainer cont = new NodeInfoContainer(t, title);
        return cont;
    }

    //TODO: Set this up so that dialogue nodes can connect to other dialogue nodes and each other
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(port => port.direction != startPort.direction && port != startPort && port.portType == startPort.portType).ToList();
    }
    
}
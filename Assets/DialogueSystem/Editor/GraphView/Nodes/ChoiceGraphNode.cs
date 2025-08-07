using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ChoiceGraphNode : DialogueGraphNode
{
    private List<ChoiceElement> choiceElements = new List<ChoiceElement>();

    private ChoiceNode cNode;

    public ChoiceGraphNode(ChoiceNode node, bool startingNode) : base(node, startingNode)
    {
        cNode = node;
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem/Editor/GraphView/USS/ChoiceGraphNode.uss");
        styleSheets.Add(styleSheet);

        VisualElement contentBox = new VisualElement();
        contentBox.AddToClassList("content-box");

        this.title = cNode.EditorId;

        Toggle isStartNode = new Toggle("Start Node?");
        isStartNode.value = startingNode;
        isStartNode.RegisterValueChangedCallback(e =>
        {
            OnStartingNodeStatusChange();
        });

        TextField editorIdField = new TextField("Editor ID:");
        editorIdField.value = cNode.EditorId;
        editorIdField.RegisterValueChangedCallback(e =>
        {
            this.title = e.newValue;
            cNode.EditorId = e.newValue;
            OnValueChange();
        });
        contentBox.Add(isStartNode);
        contentBox.Add(editorIdField);


        VisualElement choiceContainer = new VisualElement();

        Label choiceHeader = new Label("Choices:");
        choiceContainer.Add(choiceHeader);

        VisualElement choiceList = new VisualElement();

        foreach (DialogueChoice choice in cNode.choices)
        {
            ChoiceElement elm = new ChoiceElement(choice, this);
            elm.deleteCallback = () =>
            {
                cNode.choices.Remove(choice);
                choiceList.Remove(elm);
                OnValueChange();
            };
            elm.changeCallback = () => {
                OnValueChange();
            };
            choiceList.Add(elm);
            choiceElements.Add(elm);
        }
        choiceContainer.Add(choiceList);

        Button addChoiceBtn = new Button();
        addChoiceBtn.text = "ADD CHOICE";
        addChoiceBtn.RegisterCallback<ClickEvent>(e =>
        {
            var c = cNode.AddChoice();
            ChoiceElement elm = new ChoiceElement(c, this);
            choiceList.Add(elm);
            choiceElements.Add(elm);
            elm.deleteCallback = elm.deleteCallback = () =>
            {
                cNode.choices.Remove(c);
                choiceList.Remove(elm);
            };
            OnValueChange();    
        });
        choiceContainer.Add(addChoiceBtn);
        contentBox.Add(choiceContainer);


        inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(DialogueGraphNode));
        inputContainer.Add(inputPort);




        extensionContainer.Add(contentBox);

        RefreshExpandedState();
    }

    public override void ConnectNodeWithPort(Port output, Port input)
    {
        DialogueNode nextNode = ((DialogueGraphNode)input.node).node;
        choiceElements.Where(elem => elem.outPort == output).First().choice.nextNode = nextNode;
    }

    public override void DisconnectPort(Port output)
    {
        choiceElements.Where(elem => elem.outPort == output).First().choice.nextNode = null;
    }

    public override List<Edge> DrawEdges(Dictionary<string, DialogueGraphNode> nodeDict)
    {
        foreach (ChoiceElement choiceElement in choiceElements)
        {
            if (choiceElement.choice.nextNode != null)
            {
                Port input = nodeDict[choiceElement.choice.nextNode.NodeId].inputPort;
                var edge = choiceElement.outPort.ConnectTo(input);
                edgeList.Add(edge);
            }
        }
        return edgeList;
    }

    public override void RemoveIncomingConnections(Port output)
    {
        choiceElements.Where(elem => elem.outPort == output).Select(elem => elem.choice.nextNode = null);
    }
}

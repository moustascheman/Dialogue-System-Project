using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChoiceElement : VisualElement
{
    private DialogueChoice m_Choice;

    public DialogueChoice choice => m_Choice;

    public Port outPort;

    public Action deleteCallback;

    public Action changeCallback;
    public ChoiceElement(DialogueChoice thisChoice, ChoiceGraphNode owningNode)
    {
        m_Choice = thisChoice;

        AddToClassList("choice-element");

        TextField choiceText = new TextField();
        choiceText.AddToClassList("choice-text");
        Debug.Log(thisChoice);
        choiceText.value = choice.ChoiceText;
        choiceText.RegisterValueChangedCallback(e =>
        {
            this.choice.ChoiceText = e.newValue;
            changeCallback();
        });

        UnityEngine.UIElements.Button editCondButton = new UnityEngine.UIElements.Button();
        editCondButton.text = "Edit Conditions";
        editCondButton.AddToClassList("edit-condition-button");


        UnityEngine.UIElements.Button deleteButton = new UnityEngine.UIElements.Button();
        deleteButton.text = "🗑";
        deleteButton.AddToClassList("delete-button");
        deleteButton.RegisterCallback<ClickEvent>(e =>
        {
            deleteCallback();
        });

        outPort = owningNode.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(DialogueGraphNode));
        outPort.portName = "";

        Add(choiceText);
        Add(editCondButton);
        Add(deleteButton);
        Add(outPort);



    }
}

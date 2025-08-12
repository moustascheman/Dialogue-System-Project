using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueView : MonoBehaviour
{

    [SerializeField]
    private Conversation convoAsset;

    [SerializeField]
    private UIDocument DialogueUI;


    [SerializeField]
    private VisualTreeAsset choiceElement;

    private BasicDialogueController controller;




    //Visual Elements
    private VisualElement ResponseBox;
    private Label ResponseText;
    private Button ContinueButton;


    private VisualElement ChoiceBox;
    private ListView ChoiceList;


    void OnEnable()
    {
        Setup();
        LoadAsset(convoAsset);
        LoadNode(controller.currentNode);
    }

    private void Setup()
    {
        DialogueUI = GetComponent<UIDocument>();
        ResponseBox = DialogueUI.rootVisualElement.Q<VisualElement>("ResponseBox");
        ResponseText = DialogueUI.rootVisualElement.Q<Label>("ResponseText");
        ContinueButton = DialogueUI.rootVisualElement.Q<Button>("ContinueButton");

        ChoiceBox = DialogueUI.rootVisualElement.Q<VisualElement>("ChoiceBox");
    }


    private void LoadAsset(Conversation asset)
    {
        controller = new BasicDialogueController(asset);
    }


    private void LoadNode(DialogueNode current)
    {
        if (current == null)
        {
            LoadExitNode();
        }
        else if (current.GetType() == typeof(ResponseNode))
        {
            LoadResponseNode(current as ResponseNode);
        }
        else if (current.GetType() == typeof(ChoiceNode))
        {
            LoadChoiceNode(current as ChoiceNode);
        }
        else
        {
            LoadExitNode();
        }

    }

    private void HideNodeContainers()
    {
        Hide(ResponseBox);
        Hide(ChoiceBox);
        var ChoiceListView = ChoiceBox.Q<ListView>("ChoiceList");
        if (ChoiceListView != null)
        {
            ChoiceBox.Remove(ChoiceListView);
        }
    }

    private void Hide(VisualElement elem)
    {
        if (!elem.ClassListContains("Hidden"))
        {
            elem.AddToClassList("Hidden");
        }
    }


    private void LoadResponseNode(ResponseNode node)
    {
        HideNodeContainers();
        ResponseBox.RemoveFromClassList("Hidden");
        ResponseText.text = node.textValue;

        ContinueButton.RegisterCallbackOnce<ClickEvent>(e =>
        {
            LoadNode(controller.GetNextNode(0));
        });
    }


    private void LoadChoiceNode(ChoiceNode node)
    {
        HideNodeContainers();
        ChoiceBox.RemoveFromClassList("Hidden");

        List<DialogueChoice> choices = node.GetChoices();
        if (choices.Count == 0)
        {
            var exitChoice = new DialogueChoice();
            exitChoice.ChoiceText = "EXIT";
            choices.Add(exitChoice);
        }
        FillChoiceList(choices);



    }

    private void FillChoiceList(List<DialogueChoice> choices)
    {
        ChoiceList = new ListView();
        ChoiceList.name = "ChoiceList";
        ChoiceBox.Add(ChoiceList);
        ChoiceList.Clear();
        ChoiceList.makeItem = () =>
        {
            var newChoiceElement = choiceElement.Instantiate();
            return newChoiceElement;

        };

        ChoiceList.bindItem = (choiceElem, index) =>
        {
            Label tag = choiceElem.Q<Label>("DialogueTag");
            tag.text = "";
            Label choicetext = choiceElem.Q<Label>("ChoiceText");
            Debug.Log(index);
            choicetext.text = choices[index].ChoiceText;

            choiceElem.RegisterCallbackOnce<ClickEvent>(e =>
            {
                LoadNode(controller.GetNextNode(index));
            });

        };
        ChoiceList.itemsSource = choices;
    }


    private void LoadExitNode()
    {
        HideNodeContainers();
        ResponseBox.RemoveFromClassList("Hidden");
        ResponseText.text = "Time to Exit!";
        ContinueButton.text = "EXIT";

        ContinueButton.RegisterCallbackOnce<ClickEvent>(e =>
        {
            Debug.Log("EXITING");
        });

    }
}

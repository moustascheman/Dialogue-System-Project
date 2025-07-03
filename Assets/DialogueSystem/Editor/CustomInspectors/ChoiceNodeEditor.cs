using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(ChoiceNode))]
public class ChoiceNodeEditor : Editor
{
    private ChoiceNode _choiceNode;

    private Button editNameBtn;
    private Button confirmNameBtn;
    private VisualElement headerBox;
    private VisualElement editBox;



    private Button AddChoiceBtn;

    public VisualTreeAsset markup;

    public VisualTreeAsset ChoicePanel;

    private VisualElement choiceList;

    void OnEnable()
    {
        _choiceNode = target as ChoiceNode;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        markup.CloneTree(root);

        editNameBtn = root.Q<Button>("NodeIdEditBtn");
        confirmNameBtn = root.Q<Button>("NodeIdEditConfirmBtn");
        headerBox = root.Q<VisualElement>("HeaderBox");
        editBox = root.Q<VisualElement>("EditBox");
        editNameBtn.RegisterCallback<ClickEvent>(OnEditButtonClick);
        confirmNameBtn.RegisterCallback<ClickEvent>(OnEditConfirm);
        choiceList = root.Q<VisualElement>("ChoiceList");
        BuildDialogueChoiceList(choiceList);

        AddChoiceBtn = root.Q<Button>("AddChoice");
        AddChoiceBtn.RegisterCallback<ClickEvent>(OnCreateButtonClick);







        return root;
    }

    private void BuildDialogueChoiceList(VisualElement choiceList)
    {
        choiceList.Clear();
        foreach (DialogueChoice choice in _choiceNode.choices)
        {
            VisualElement Container = new VisualElement();
            ChoicePanel.CloneTree(Container);
            PropertyField choiceProp = Container.Q<PropertyField>();
            Button deleteButton = Container.Q<Button>();
            deleteButton.text = "Delete";
            deleteButton.RegisterCallback<ClickEvent>((evt) => DeleteChoice(choice));
            choiceList.Add(Container);
        }

        
    }



    private void OnEditButtonClick(ClickEvent e)
    {
        headerBox.AddToClassList("HeaderHide");
        editBox.RemoveFromClassList("HeaderHide");
    }

    private void OnEditConfirm(ClickEvent e)
    {
        editBox.AddToClassList("HeaderHide");
        headerBox.RemoveFromClassList("HeaderHide");
    }

    private void DeleteChoice(DialogueChoice choice)
    {
        Debug.Log("HIT");
        _choiceNode.choices.Remove(choice);
        AssetDatabase.RemoveObjectFromAsset(choice);
        UnityEditor.EditorUtility.SetDirty(choice);
        AssetDatabase.SaveAssets();
        BuildDialogueChoiceList(choiceList);
    }


    private void OnCreateButtonClick(ClickEvent e)
    {
        DialogueChoice newChoice = DialogueChoice.CreateInstance<DialogueChoice>();
        newChoice.name = "DialogueChoice_" + GUID.Generate();
        AssetDatabase.AddObjectToAsset(newChoice, _choiceNode);
        AssetDatabase.SaveAssetIfDirty(_choiceNode);
        _choiceNode.choices.Add(newChoice);
        BuildDialogueChoiceList(choiceList);
    }

}
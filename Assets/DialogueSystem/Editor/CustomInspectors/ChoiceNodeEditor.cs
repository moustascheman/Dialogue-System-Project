using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System.Linq;
using System;
using Unity.Properties;

/*[CustomEditor(typeof(ChoiceNode))]
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


    private SerializedObject so;
    void OnEnable()
    {
        _choiceNode = target as ChoiceNode;
    }

    public override VisualElement CreateInspectorGUI()
    {

        so = serializedObject;
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
        int i = 0;
        Debug.Log(_choiceNode);
        foreach (DialogueChoice choice in _choiceNode.choices)
        {

            VisualElement Container = new VisualElement();
            ChoicePanel.CloneTree(Container);


            Button deleteButton = Container.Q<Button>();
            PropertyField choiceField = Container.Q<PropertyField>("ChoiceField");
            string bindingPath = $"choices.Array.data[{i}]";
            serializedObject.Update();
            choiceField.BindProperty(serializedObject.FindProperty(bindingPath));


            deleteButton.text = "Delete";
            deleteButton.RegisterCallback<ClickEvent>((evt) => DeleteChoice(choice));
            choiceList.Add(Container);
            i++;

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
        _choiceNode.choices.Remove(choice);
        AssetDatabase.RemoveObjectFromAsset(choice);
        UnityEditor.EditorUtility.SetDirty(choice);
        AssetDatabase.SaveAssets();
        so.Update();
        BuildDialogueChoiceList(choiceList);
    }


    private void OnCreateButtonClick(ClickEvent e)
    {
        DialogueChoice newChoice = DialogueChoice.CreateInstance<DialogueChoice>();
        newChoice.name = "DialogueChoice_" + GUID.Generate();
        AssetDatabase.AddObjectToAsset(newChoice, _choiceNode);
        AssetDatabase.SaveAssetIfDirty(_choiceNode);
        so.Update();
        _choiceNode.choices.Add(newChoice);
        BuildDialogueChoiceList(choiceList);
    }

}*/
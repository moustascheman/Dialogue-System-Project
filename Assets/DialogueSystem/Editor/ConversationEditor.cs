using Codice.CM.Client.Gui;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Conversation))]
public class ConversationEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        Button editButton = new Button();
        editButton.text = "Edit Convo Tree";
        editButton.RegisterCallback<ClickEvent>(e =>
        {
            ConversationEditorWindow.Open(target as Conversation);
        });
        root.Add(editButton);

        PropertyField nodeList = new PropertyField();
        nodeList.bindingPath = "nodes";

        root.Add(nodeList);


        return root;   
    }
}

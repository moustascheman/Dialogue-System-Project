using Codice.CM.Client.Gui;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Conversation))]
public class ConversationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit Conversation Tree"))
        {
            ConversationEditorWindow.Open((Conversation)target);
        }
        base.OnInspectorGUI();
    }
}

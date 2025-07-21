using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


//[CustomPropertyDrawer(typeof(DialogueChoice))]
public class DialogueChoicePropertyDrawer : PropertyDrawer
{
    public VisualTreeAsset ChoiceTemplate;
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        SerializedObject so = new(property.objectReferenceValue);

        VisualElement root = new VisualElement();
        TextField ChoiceText = new TextField();
        ChoiceText.label = "Choice Text:";
        ChoiceText.BindProperty(so.FindProperty("ChoiceText"));
        root.Add(ChoiceText);
        PropertyField NextNode = new PropertyField();
        NextNode.label = "Connected Node:";
        NextNode.BindProperty(so.FindProperty("nextNode"));
        root.Add(NextNode);

        return root;
    }


}

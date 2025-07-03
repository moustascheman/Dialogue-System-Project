using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

[CustomEditor(typeof(ResponseNode))]
public class ResponseNodeEditor : Editor
{
    private Button editNameBtn;
    private Button confirmNameBtn;
    private VisualElement headerBox;
    private VisualElement editBox;

    public VisualTreeAsset markup;
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



        return root;
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

}

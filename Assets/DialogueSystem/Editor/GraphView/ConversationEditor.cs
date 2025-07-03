using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ConversationEditor : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/ConversationEditor")]
    public static void OpenWindow()
    {
        ConversationEditor wnd = GetWindow<ConversationEditor>();
        wnd.titleContent = new GUIContent("Conversation Editor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);

    }
}

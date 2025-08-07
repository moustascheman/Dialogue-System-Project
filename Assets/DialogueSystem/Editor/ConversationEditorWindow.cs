using Mono.Cecil;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class ConversationEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private Conversation currentConversation;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);
        var graph = this.rootVisualElement.Q<ConvoGraphview>();
        graph.window = this;



    }

    private void OnGUI()
    {
        if (EditorUtility.IsDirty(currentConversation))
        {
            hasUnsavedChanges = true;
        }
        else
        {
            hasUnsavedChanges = false;
        }
    }

    private void OnEnable()
    {
        if (currentConversation != null)
        {
            RenderGraph();
        }
    }

    public static void Open(Conversation target)
    {
        ConversationEditorWindow[] windows = Resources.FindObjectsOfTypeAll<ConversationEditorWindow>();
        bool WinExists = false;
        foreach (ConversationEditorWindow win in windows)
        {
            if (win.currentConversation == target)
            {
                win.Focus();
                WinExists = true;
            }
        }
        if (!WinExists)
        {
            ConversationEditorWindow window = CreateWindow<ConversationEditorWindow>(typeof(ConversationEditorWindow), typeof(SceneView));
            window.titleContent = new GUIContent($"Conversation Editor - {target.name}");
            window.currentConversation = target;
            window.RenderGraph();
        }

    }

    private void RenderGraph()
    {
        var graph = this.rootVisualElement.Q<ConvoGraphview>();
        graph.obj = new SerializedObject(currentConversation);
        graph.convo = currentConversation;
        graph.RenderExistingNodes();
    }
}



using Mono.Cecil;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class ConversationEditorWindow : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;
    private Conversation conversation;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        m_VisualTreeAsset.CloneTree(root);
        var graph = this.rootVisualElement.Q<ConvoGraphview>();
        graph.window = this;
        


    }

    public static void Open(Conversation target)
    {
          ConversationEditorWindow wnd = GetWindow<ConversationEditorWindow>();
          wnd.titleContent = new GUIContent(target.name + " - Conversation Editor");
          var graph = wnd.rootVisualElement.Q<ConvoGraphview>();
          graph.obj = new SerializedObject(target);

          graph.convo = target;
        
        //Really dislike this. I should probably call the graph constructor here instead of doing this
        graph.RenderExistingNodes();
    }
}

using UnityEditor;
using UnityEngine;

namespace DialogueSystem.Editor
{

    public class DialogueEditorWindow : EditorWindow
    {
        [SerializeField]
        private Conversation currentConversation;


        public static void Open(Conversation convoAsset)
        {
            
        }

    }
}
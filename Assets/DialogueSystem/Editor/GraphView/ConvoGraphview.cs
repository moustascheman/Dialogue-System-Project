using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using NUnit.Framework.Constraints;
using UnityEditor;

public class ConvoGraphview : GraphView
{
    
    
    public new class UxmlFactory : UxmlFactory<ConvoGraphview, GraphView.UxmlTraits> { }
    public ConvoGraphview()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        StyleSheet styles = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem/Editor/GraphView/GridStyleSheet.uss");
        styleSheets.Add(styles);
    }

}

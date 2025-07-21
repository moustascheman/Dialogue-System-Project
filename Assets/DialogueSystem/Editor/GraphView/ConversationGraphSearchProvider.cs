using System;
using System.Collections.Generic;
using System.Reflection;
using Codice.CM.WorkspaceServer.DataStore;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UIElements;

public struct NodeInfoContainer
{
    public Type nodeType;
    public string title;

    public NodeInfoContainer(Type nodeType, string title)
    {
        this.nodeType = nodeType;
        this.title = title;
        
    }
}


public class ConversationGraphSearchProvider : ScriptableObject, ISearchWindowProvider
{



    public ConvoGraphview graphview;
    public VisualElement target;
    

   
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchTree = new List<SearchTreeEntry>();
        searchTree.Add(new SearchTreeGroupEntry(new GUIContent("Nodes"), 0));

        //
        List<NodeInfoContainer> nodeTypes = new List<NodeInfoContainer>()
        {
            new NodeInfoContainer(typeof(ResponseNode), "Response Node"),
            new NodeInfoContainer(typeof(ChoiceNode), "Choice Node")
        };

        foreach (NodeInfoContainer nType in nodeTypes)
        {
            SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(nType.title));
            entry.level = 1;
            entry.userData = nType;
            searchTree.Add(entry);
        }

        return searchTree;

    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var windowMousePos = graphview.ChangeCoordinatesTo(graphview, context.screenMousePosition - graphview.window.position.position);
        var graphMousePos = graphview.contentViewContainer.WorldToLocal(windowMousePos);

        NodeInfoContainer elem = (NodeInfoContainer)SearchTreeEntry.userData;
        graphview.AddNode(elem, graphMousePos);
        return true;
        
    }
}

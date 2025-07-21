using System;
using UnityEngine;

public class DialogueNodeAttribute : Attribute
{
    private string m_nodeTitle;
    private string m_menuItem;

    public string title => m_nodeTitle;
    public string menmuItem => m_menuItem;

    public DialogueNodeAttribute(string title, string menmuItem = "")
    {
        m_nodeTitle = title;
        m_menuItem = menmuItem;
    }
    
}

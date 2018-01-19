using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.TreeViewExamples;
using System;

[Serializable]
public class UICreateElement : TreeElement {
    static int Index;


    [SerializeField]
    public bool m_Write;
    [SerializeField]
    public string m_Rename;

    public UICreateElement(bool write,string name, int depth, int id) : base (name, depth, id)
	{
        m_Write = write;
        m_Rename = name;
    }

    public static List<UICreateElement> CreateUITransformTree(Transform tra,ref Dictionary<Transform,UICreateElement> traElementDic) {
        Index = 0;

        var treeElements = new List<UICreateElement>();

        if(traElementDic==null)
            traElementDic = new Dictionary<Transform, UICreateElement>();

        var root = new UICreateElement(tra.gameObject.activeInHierarchy, tra.name, -1, Index);
        treeElements.Add(root);
        traElementDic.Add(tra, root);

        for (int i = 0; i < tra.childCount; i++) {
            AddUIChildElement(tra.GetChild(i), root.depth, treeElements,traElementDic);
        }

        return treeElements;
    }

    public static void AddUIChildElement(Transform tra, int depth,List<UICreateElement> treeElements,Dictionary<Transform,UICreateElement> dic) {
        var element = new UICreateElement(tra.gameObject.activeInHierarchy, tra.name, depth + 1, ++Index);
        treeElements.Add(element);
        dic.Add(tra, element);

        for (int i = 0; i < tra.childCount; i++) {
            AddUIChildElement(tra.GetChild(i), element.depth, treeElements,dic);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node{
    public Rect rect;
    public string title;

    public ConnectionPoint inPoint;
    public ConnectionPoint outPoint;

    bool isDragged=false;
    public bool isSelected;

    public GUIStyle style;
    public GUIStyle defaultNodeStyle;
    public GUIStyle selectedNodeStyle;

    public Action<Node> OnRemoveNode;
    public Node(Vector2 position, float width, float height,GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) {
        rect = new Rect(position.x, position.y, width, height);
        style = nodeStyle;
        inPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle, OnClickInPoint);
        outPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle, OnClickOutPoint);

        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;

        OnRemoveNode = OnClickRemoveNode;
    }

    public void Drag(Vector2 delta) {
        rect.position += delta;
    }

    public void Draw() {
        GUI.Box(rect, title, style);
        inPoint.Draw();
        outPoint.Draw();
    }

    public bool ProcessEvents(Event e) {

        switch (e.type) {
            case EventType.MouseDown:
                if (e.button == 0) {
                    if (rect.Contains(e.mousePosition)) {
                        isDragged = true;
                        GUI.changed = true;

                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else {
                        GUI.changed = true;

                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                }
                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition)) {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged) {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu() {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode() {
        if (OnRemoveNode != null) {
            OnRemoveNode(this);
        }
    }
}

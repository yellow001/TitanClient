using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlotAssetUIWindow : EditorWindow {

    public static BasePlotDataAsset asset;

    [MenuItem("YUtil/Show Plot Win")]
    public static void ShowWin() {
        PlotAssetUIWindow win = GetWindow<PlotAssetUIWindow>();
        //asset = a;
        win.Show();
    }


    void OnGUI() {
        Handles.BeginGUI();
        Handles.color = Color.red;
        Handles.DrawLine(new Vector3(0, 0), new Vector3(300, 300));
        Handles.EndGUI();
    }
}

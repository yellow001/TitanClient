/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

public class PlayMakerNavMeshAreaMaskField {

	
	public List<string> areas;
	public List<int> areaNumbers;
	public string[] areaNames;
	public long lastUpdateTick;
	
	// http://answers.unity3d.com/questions/42996/how-to-create-layermask-field-in-a-custom-editorwi.html
	
	/** Displays a LayerMask field.
	 * \param showSpecial Use the Nothing and Everything selections
	 * \param selected Current LayerMask
	 * \version Unity 3.5 and up will use the EditorGUILayout.MaskField instead of a custom written one.
	 */
	public LayerMask AreaMaskField (string label, LayerMask selected, bool showSpecial) {
		
		//Unity 3.5 and up
		
		if (areas == null || (System.DateTime.Now.Ticks - lastUpdateTick > 10000000L && Event.current.type == EventType.Layout)) {
			lastUpdateTick = System.DateTime.Now.Ticks;
			if (areas == null) {
				areas = new List<string>();
				areaNumbers = new List<int>();
				areaNames = new string[4];
			} else {
				areas.Clear ();
				areaNumbers.Clear ();
			}
			
			string[] _layers = GameObjectUtility.GetNavMeshAreaNames();
			
			int emptyLayers = 0;
			for (int i=0;i<_layers.Length;i++) {
				string layerName = _layers[i];
				
				if (layerName != "") {
					
					for (;emptyLayers>0;emptyLayers--) areas.Add ("Area "+(i-emptyLayers));
					areaNumbers.Add (i);
					areas.Add (layerName);
				} else {
					emptyLayers++;
				}
			}
			
			if (areaNames.Length != areas.Count) {
				areaNames = new string[areas.Count];
			}
			for (int i=0;i<areaNames.Length;i++) areaNames[i] = areas[i];
		}
		
		selected.value =  EditorGUILayout.MaskField (label,selected.value,areaNames);

		//GUILayout.Label(" "+selected.value,GUILayout.ExpandWidth(false));

		return selected;
	}
	

}

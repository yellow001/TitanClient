using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatColorCtrl : MonoBehaviour {

    public Renderer render;

    [System.Serializable]
    public struct MatColorData {
        public int matID;
        public string[] propertys;
    }

    public MatColorData[] matList;

    public void SetColor(string[] color) {
        for (int i = 0; i < matList.Length; i++) {
            if (i < color.Length) {
                foreach (var item in matList[i].propertys) {
                    Color c;
                    ColorUtility.TryParseHtmlString(color[i], out c);
                    render.materials[matList[i].matID].SetColor(item, c);
                }
            }
        }
    }
}

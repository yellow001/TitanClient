using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class YUIEditor : MonoBehaviour {

    public static readonly string UITemplePath = Application.dataPath + "/YUtil/Tools/UGUIScriptCreator/Scripts/Template/BaseUITemple.cs";

    static string content = "";
    static Dictionary<string,Type> memberTypes = new Dictionary<string, Type>();
    static Dictionary<string, Transform> memberTras = new Dictionary<string, Transform>();
    static Dictionary<Transform, UICreateElement> traElementDic = new Dictionary<Transform, UICreateElement>();
    static string m_path;
    static string m_lastDicPath;

    [MenuItem("YUtil/Create Normal UI Script")]
    public static void CreateUI() {
        Clear();

        Transform[] tras = Selection.transforms;

        if (tras.Length == 0) {
            EditorUtility.DisplayDialog("提醒", "请选择一个UI物体", "确定");
            return;
        }

        if (tras.Length > 1) {
            EditorUtility.DisplayDialog("提醒", "只能选择一个同层级物体", "确定");
            return;
        }

        if (!(tras[0] is RectTransform)) {
            EditorUtility.DisplayDialog("提醒", "只能UI物体", "确定");
            return;
        }

        //m_path = AppConst.UIScriptPath + tras[0].name + ".cs";
        m_path=EditorUtility.SaveFilePanel("选择保存路径", m_lastDicPath, tras[0].name, "cs");

        if (string.IsNullOrEmpty(m_path)) { return; }

        //if (File.Exists(m_path)) {
        //    if (!EditorUtility.DisplayDialog("提醒", "已存在该UI脚本，是否覆盖？", "是", "否")) {
        //        return;
        //    }
        //}
        m_lastDicPath = m_path;
        //WriteScript(path, tras[0]);
        MultiUICreateWindow.GetWindow(tras[0]);
    }


    public static void WriteScript(Transform root,Dictionary<Transform,UICreateElement> dic) {

        if (!File.Exists(UITemplePath)) {
            EditorUtility.DisplayDialog("提醒", "额。。。模板 BaseUITemple.cs 没找到，请到 YUtil/AppConst.cs 设置一下 UITemplePath", "确定");
            return;
        }

        StreamReader sr = new StreamReader(UITemplePath,Encoding.UTF8);
        content = sr.ReadToEnd();
        sr.Close();

        if (!CheckTempleFile(content)) {
            return;
        }

        //content= content.Replace("BaseUITemple", root.name);
        //AddMember(root);
        //AddInit(root);
        //AddUpdateView();
        //Debug.Log(content);
        //Clear();

        StreamWriter sw = new StreamWriter(m_path, false, Encoding.UTF8);
        content = content.Replace("BaseUITemple", root.name);

        traElementDic = dic;

        AddMember(root);
        AddInit(root);
        AddEvent();
        AddUpdateView();

        sw.Write(content);
        sw.Close();
        Debug.Log(string.Format("生成成功，路径:{0}", m_path));
        Clear();
        AssetDatabase.Refresh();
    }

    static void AddMember(Transform root) {

        string[] contents = content.Split(new string[] { "//@member" }, StringSplitOptions.RemoveEmptyEntries);

        Transform[] tras = Selection.GetTransforms(SelectionMode.Deep);

        foreach (var item in tras) {

            if (traElementDic.ContainsKey(item) && !traElementDic[item].m_Write) {
                continue;
            }

            if (!(item is RectTransform)) {
                continue;
            }
            Type t = GetMemberType(item);
            if (t != null) {
                string n = traElementDic[item].m_Rename;
                if (item == root) {
                    n = "root";
                }
                if (memberTypes.ContainsKey(traElementDic[item].m_Rename)) {
                    n += memberTypes.Count + "";
                }
                memberTypes.Add(n, t);
                memberTras.Add(n, item);
                string[] tempType = t.ToString().Split('.');
                string m = "public " + tempType[tempType.Length - 1] + " " + n + ";\n";
                contents[0] += "\t" + "[HideInInspector]\n";
                contents[0] += "\t" + m;
            }
            else if (traElementDic[item].m_Write) {
                string n = traElementDic[item].m_Rename;
                if (item == root) {
                    n = "root";
                }
                if (memberTypes.ContainsKey(traElementDic[item].m_Rename)) {
                    n += memberTypes.Count + "";
                }
                memberTypes.Add(n, typeof(Transform));
                memberTras.Add(n, item);
                string m = "public Transform " + n + ";\n";
                contents[0] += "\t" + "[HideInInspector]\n";
                contents[0] += "\t" + m;
            }
        }

        contents[0] += "\t" + "[HideInInspector]\n";
        contents[0] += "\t" + "public Transform tra;\n";

        content = contents[0] + contents[1];
    }

    static void AddInit(Transform root) {
        string[] contents = content.Split(new string[] { "//@Init" }, StringSplitOptions.RemoveEmptyEntries);

        contents[0] += "\t\t"+"tra=transform;\n";

        foreach (var item in memberTras) {
            string c = GetMemberPath(item.Key,root);
            string[] t = memberTypes[item.Key].ToString().Split('.');

            if (c != null) {
                if (Type.GetType(t[t.Length - 1]) == typeof(Transform)) {
                    c = item.Key + "=tra.Find(\"" + c + "\");\n";
                }
                else {
                    c = item.Key + "=tra.Find(\"" + c + "\").GetComponent<" + t[t.Length - 1] + ">();\n";
                }
            } else {

                c= item.Key + "=GetComponent <" + t[t.Length - 1] + ">();\n";

            }

            contents[0] += "\t\t" + c;
        }

        content = contents[0] + contents[1];

    }

    static void AddEvent() {
        string[] contents = content.Split(new string[] { "//@AddEvent" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in memberTypes) {
            string c = "";
            if (item.Value == typeof(Button)) {
                c = "//" + item.Key + ".onClick.AddListener();" + "\n";
            }
            else if (item.Value == typeof(Slider)) {
                c = "//" + item.Key + ".onValueChanged.AddListener();" + "\n";
            }
            else if (item.Value == typeof(InputField)) {
                c= "//" + item.Key + ".onEndEdit.AddListener();" + "\n";
            } 
            else if (item.Value == typeof(Toggle)|| item.Value == typeof(Dropdown)) {
                c = "//" + item.Key + ".onValueChanged.AddListener();" + "\n";
            } 
            contents[0] += "\t\t" + c;
        }

        content = contents[0] + contents[1];
    }

    static void AddUpdateView() {
        string[] contents = content.Split(new string[] { "//@UpdateView" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in memberTypes) {
            string c = "";
            //if (item.Value == typeof(Button)) {
            //    c = "//" + item.Key + ".onClick.AddListener();" + "\n";
            //}
            if (item.Value == typeof(Slider)) {
                c = "//" + item.Key + ".maxValue=;" + "\n"+
                    "\t//" + item.Key + ".value=;" + "\n";
            }
            else if (item.Value == typeof(InputField)) {
                c = "//" + item.Key + ".text=;" + "\n";
            }
            else if (item.Value == typeof(Toggle) || item.Value == typeof(Dropdown)) {
                c = "//" + item.Key + ".isOn=;" + "\n";
            }
            else if (item.Value == typeof(Image)) {
                c = "//" + item.Key + ".sprite=null;" + "\n";
            }
            else if (item.Value == typeof(Text)) {
                c = "//" + item.Key + ".text=\"\";" + "\n";
            }
            else if (item.Value == typeof(RawImage)) {
                c = "//" + item.Key + ".texture=null;" + "\n";
            }
            contents[0] += "\t\t" + c;
        }

        content = contents[0] + contents[1];
    }

    static bool CheckTempleFile(string content) {
        if (!content.Contains("//@member")) {
            EditorUtility.DisplayDialog("提醒", "模板文件缺少 //@member 标记，请修改模板文件", "确定");
            return false;
        }

        if (!content.Contains("//@Init")) {
            EditorUtility.DisplayDialog("提醒", "模板文件缺少 //@Init 标记，请修改模板文件", "确定");
            return false;
        }

        if (!content.Contains("//@UpdateView")) {
            EditorUtility.DisplayDialog("提醒", "模板文件缺少 //@UpdateView 标记，请修改模板文件", "确定");
            return false;
        }

        return true;
    }

    static Type GetMemberType(Transform tra) {

        if (tra.GetComponent<Selectable>()) {

            Selectable s = tra.GetComponent<Selectable>();

            if (s is Button) {
                return typeof(Button);
            } else if (s is Slider) {
                return typeof(Slider);
            } else if (s is Toggle) {
                return typeof(Toggle);
            } else if (s is Dropdown) {
                return typeof(Dropdown);
            } else if (s is InputField) {
                return typeof(InputField);
            }


        } else if (tra.GetComponent<Graphic>()) {

            //if (tra.parent!=null&&tra.parent.GetComponent<Selectable>()) {
            //    return null;
            //}

            Graphic g = tra.GetComponent<Graphic>();

            if (g is Image) {
                return typeof(Image);
            } else if (g is RawImage) {
                return typeof(RawImage);
            } else if (g is Text) {
                return typeof(Text);
            }

        }


        return null;
    }

    static string GetMemberPath(string name,Transform root) {
        Transform p = memberTras[name];
        string path = p.name;

        if (p == root) {
            return null;
        }

        while (p.parent != null&&p.parent != root) {
            p = p.parent;
            path = p.name + "/" + path;
        }
        return path;
    }

    static void Clear() {
        content = "";
        m_path = "";
        memberTypes.Clear();
        memberTras.Clear();
        traElementDic.Clear();
    }
}

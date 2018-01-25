using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasePlotData {
    public List<string> roleNames=new List<string>();
    public List<EM_RoleName> em_roleNames=new List<EM_RoleName>();
    public List<Sprite> Icons=new List<Sprite>();
    public List<string> contents=new List<string>();
    public List<AudioClip> audios=new List<AudioClip>();

    static Dictionary<EM_RoleName, string> roleNameDic = new Dictionary<EM_RoleName, string>() {
        { EM_RoleName.None,""},
        { EM_RoleName.小明,"小明"},
        { EM_RoleName.小红,"小红"},
    };
    

    public BasePlotData() {
        roleNames.Add(((EM_RoleName)0).ToString());
        em_roleNames.Add(EM_RoleName.None);
        Icons.Add(null);
        contents.Add("");
        audios.Add(null);
    }
    //public BasePlotData(string rname, string con) {
    //    System.re
    //    roleName = rname;
    //    content = con;
    //}

    //public BasePlotData(string rname,string con,AudioClip au, Sprite icon) {
    //    roleName = rname;
    //    content = con;
    //    audio = au;
    //    this.icon = icon;
    //}

    public virtual void SetData(IList<string> rnames,IList<EM_RoleName> em_rnames, IList<string> cons, IList<AudioClip>  aus, IList<Sprite> icons) {
        roleNames.AddRange(rnames);
        em_roleNames.AddRange(em_rnames);
        contents.AddRange(cons);
        audios.AddRange(aus);
        Icons.AddRange(icons);
    }

    public virtual void SetData<T>(T data)where T:BasePlotData {
        roleNames = data.roleNames;
        em_roleNames = data.em_roleNames;
        contents = data.contents;
        audios = data.audios;
        Icons = data.Icons;
    }

    public virtual void AddData(string rname, string con, AudioClip au, Sprite icon) {
        roleNames.Add(rname);
        em_roleNames.Add(EM_RoleName.None);
        contents.Add(con);
        audios.Add(au);
        Icons.Add(icon);
    }

    public virtual void DeleteData(int index) {
        if (roleNames.Count > index) {
            roleNames.RemoveAt(index);
            em_roleNames.RemoveAt(index);
            contents.RemoveAt(index);
            audios.RemoveAt(index);
            Icons.RemoveAt(index);
        }
    }

    public string GetRoleName(int index=0) {
        if (roleNames!=null&&roleNames.Count > index) {
            return roleNames[index];
        }

        if (em_roleNames != null && em_roleNames.Count > index) {
            return roleNameDic[em_roleNames[index]];
        }

        return roleNameDic[EM_RoleName.None];
    }

    public string GetRoleNameEditorGUI(int index = 0) {

        if (em_roleNames != null && em_roleNames.Count > index) {
            return roleNameDic[em_roleNames[index]];
        }

        return roleNameDic[EM_RoleName.None];
    }
}


public enum EM_RoleName:int {
    None=0,
    小明=1,
    小红=2,
}
using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using UnityEngine;

namespace ServerSimple.Data
{
    /// <summary>
    /// 该数据其实应该配表读取
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class BaseModelData
    {
        [ProtoMember(1)]
        public string modelName;
        [ProtoMember(2)]
        public GrowValue hpGrow;
        [ProtoMember(3)]
        public GrowValue energyGrow;
        [ProtoMember(4)]
        public GrowValue speedGrow;
        [ProtoMember(5)]
        public GrowValue expGrow;
        [ProtoMember(6)]
        public GrowValue atkGrow;
        [ProtoMember(7)]
        public GrowValue defendGrow;

        [ProtoMember(8)]
        public int modelID;

        public BaseModelData() { }

        public BaseModelData(int id,string mName, GrowValue hp, GrowValue energy, GrowValue speed, GrowValue exp, GrowValue atk,GrowValue defend) {
            this.modelID = id;
            modelName = mName;
            hpGrow = hp;
            energyGrow = energy;
            speedGrow = speed;
            expGrow = exp;
            atkGrow = atk;
            defendGrow = defend;
        }
    }

    [Serializable]
    [ProtoContract]
    public class GrowValue {
        [ProtoMember(1)]
        public int baseValue;
        [ProtoMember(2)]
        public int growValue;
        [ProtoMember(3)]
        public int currentValue;

        public GrowValue() { }

        public GrowValue(int bValue,int gValue,int cValue=0) {
            baseValue = bValue;
            growValue = gValue;
            currentValue = cValue;
        }

        /// <summary>
        /// 初始化当前值，交由外部处理
        /// </summary>
        /// <param name="fun"></param>
        public void InitCurrentValue(Func<GrowValue,int> fun) {
            if (fun != null) {
                currentValue = fun(this);
            }
            else {
                currentValue = 0;
            }
        }
    }

    [Serializable]
    [ProtoContract]
    public class Vector3Ex {
        [ProtoMember(1)]
        public float x;
        [ProtoMember(2)]
        public float y;
        [ProtoMember(3)]
        public float z;

        public Vector3Ex() { }
        public Vector3Ex(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3Ex Zero {
            get {
                return new Vector3Ex(0, 0, 0);
            }
        }

        public Vector3Ex(Vector3 v) {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3 ToVec3() {
            return new Vector3(x, y, z);
        }
    }
}

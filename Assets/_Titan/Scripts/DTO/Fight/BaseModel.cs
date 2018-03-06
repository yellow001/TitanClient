using ProtoBuf;
using ServerSimple.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class BaseModel {

        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public int level;
        [ProtoMember(3)]
        public string mName;
        [ProtoMember(4)]
        public int hp;
        [ProtoMember(5)]
        public int atk;
        [ProtoMember(6)]
        public int energy;
        [ProtoMember(7)]
        public int speed;
        [ProtoMember(8)]
        public int exp;
        [ProtoMember(9)]
        public int defend;

        [ProtoMember(10)]
        public BaseModelData data;

        [ProtoMember(11)]
        public Vector3Ex position;

        [ProtoMember(12)]
        public Vector3Ex rotation;


        public BaseModel() { }

        public BaseModel(int id, int level, string name, BaseModelData d,Vector3Ex p,Vector3Ex r) {
            this.id = id;
            this.level = level;
            this.data = d;
            position = p;
            rotation = r;
            InitData();
        }

        void InitData (){
            data.hpGrow.InitCurrentValue(CommonInit);
            data.atkGrow.InitCurrentValue(CommonInit);
            data.defendGrow.InitCurrentValue(CommonInit);
            data.energyGrow.InitCurrentValue(CommonInit);
            data.speedGrow.InitCurrentValue(CommonInit);

            hp = data.hpGrow.currentValue;
            atk = data.atkGrow.currentValue;
            defend = data.defendGrow.currentValue;
            energy = data.energyGrow.currentValue;
            speed = data.speedGrow.currentValue;
        }

        /// <summary>
        /// 初始化方法应该由一个具体的静态类或静态方法控制，此处为了方便直接定义一个通用的
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        int CommonInit(GrowValue v) {
            return v.baseValue + level * v.growValue;
        }

        public bool IsDead() {
            return hp <= 0;
        }
    }
}

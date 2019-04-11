//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Halo
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 名称
        /// </summary>
        public int Name;

        /// <summary>
        /// 光环图标
        /// </summary>
        public string HaloIcon;

        /// <summary>
        /// 描述
        /// </summary>
        public int Describe;

        /// <summary>
        /// 解锁条件描述
        /// </summary>
        public int UnlockDes;

        /// <summary>
        /// 升级描述
        /// </summary>
        public int LvUpDes;

        /// <summary>
        /// 当前等级
        /// </summary>
        public int Lv;

        /// <summary>
        /// 下一级ID
        /// </summary>
        public int NextLv;

        /// <summary>
        /// 数值1
        /// </summary>
        public int AttrValue1;

        /// <summary>
        /// 数值2
        /// </summary>
        public int AttrValue2;

        /// <summary>
        /// 数值3
        /// </summary>
        public int AttrValue3;

        /// <summary>
        /// 数值4
        /// </summary>
        public int AttrValue4;

        /// <summary>
        /// 技能解锁条件类型0：没有开启条件1：技能满级
        /// </summary>
        public int UnlockCondition;

        /// <summary>
        /// 类型为1时:光环技能ID
        /// </summary>
        public int Value1;

        /// <summary>
        /// 类型为1时:光环技能ID
        /// </summary>
        public int Value2;

        /// <summary>
        /// 升级条件类型1：需要XX类英雄数量2：需求道具数量
        /// </summary>
        public int LvUpCondition;

        /// <summary>
        /// 类型为1时：写英雄数量类型为2时：写道具ID
        /// </summary>
        public int Value3;

        /// <summary>
        /// 类型为2时：道具数量
        /// </summary>
        public int Value4;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Halo:"
    
            +"    ID:"+ID

            +"    Name:"+Name

            +"    HaloIcon:"+HaloIcon

            +"    Describe:"+Describe

            +"    UnlockDes:"+UnlockDes

            +"    LvUpDes:"+LvUpDes

            +"    Lv:"+Lv

            +"    NextLv:"+NextLv

            +"    AttrValue1:"+AttrValue1

            +"    AttrValue2:"+AttrValue2

            +"    AttrValue3:"+AttrValue3

            +"    AttrValue4:"+AttrValue4

            +"    UnlockCondition:"+UnlockCondition

            +"    Value1:"+Value1

            +"    Value2:"+Value2

            +"    LvUpCondition:"+LvUpCondition

            +"    Value3:"+Value3

            +"    Value4:"+Value4
;
        }
    }
}

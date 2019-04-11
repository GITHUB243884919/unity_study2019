//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GirlSkill
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 技能名称
        /// </summary>
        public int SkillName;

        /// <summary>
        /// 技能升级类型
        /// </summary>
        public int SkillType;

        /// <summary>
        /// 技能描述
        /// </summary>
        public int SkillDes;

        /// <summary>
        /// 等级上限
        /// </summary>
        public int LvMax;

        /// <summary>
        /// 增量数据类型
        /// 0：整数
        /// 1：百分比
        /// </summary>
        public int ValueType;

        /// <summary>
        /// 武力属性类型
        /// </summary>
        public int AttrType1;

        /// <summary>
        /// 智力属性类型
        /// </summary>
        public int AttrType2;

        /// <summary>
        /// 政治属性类型
        /// </summary>
        public int AttrType3;

        /// <summary>
        /// 魅力属性类型
        /// </summary>
        public int AttrType4;

        /// <summary>
        /// 每级增长量
        /// 若为百分比则/10000
        /// </summary>
        public int Growup;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GirlSkill:"
    
            +"    ID:"+ID

            +"    SkillName:"+SkillName

            +"    SkillType:"+SkillType

            +"    SkillDes:"+SkillDes

            +"    LvMax:"+LvMax

            +"    ValueType:"+ValueType

            +"    AttrType1:"+AttrType1

            +"    AttrType2:"+AttrType2

            +"    AttrType3:"+AttrType3

            +"    AttrType4:"+AttrType4

            +"    Growup:"+Growup
;
        }
    }
}

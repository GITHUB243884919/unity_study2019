//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GirlSkillExp
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 技能类型
        /// </summary>
        public int SkillType;

        /// <summary>
        /// 等级
        /// </summary>
        public int SkillLv;

        /// <summary>
        /// 升到下一级消耗
        /// </summary>
        public int Consume;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GirlSkillExp:"
    
            +"    ID:"+ID

            +"    SkillType:"+SkillType

            +"    SkillLv:"+SkillLv

            +"    Consume:"+Consume
;
        }
    }
}

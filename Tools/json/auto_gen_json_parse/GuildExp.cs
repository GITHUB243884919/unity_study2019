//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GuildExp
    {
        #region field

        /// <summary>
        /// 公会等级
        /// </summary>
        public int ID;

        /// <summary>
        /// 升级经验
        /// </summary>
        public int Exp;

        /// <summary>
        /// 公会人数上限
        /// </summary>
        public int Number;

        /// <summary>
        /// 名胜建设是否开启
        /// </summary>
        public int BuildOpenLv;

        /// <summary>
        /// 会长人数
        /// </summary>
        public int PresidentNum;

        /// <summary>
        /// 副会长人数
        /// </summary>
        public int VicePresidentNum;

        /// <summary>
        /// 精英人数
        /// </summary>
        public int EliteNum;

        /// <summary>
        /// 成员人数
        /// </summary>
        public int MemberNum;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GuildExp:"
    
            +"    ID:"+ID

            +"    Exp:"+Exp

            +"    Number:"+Number

            +"    BuildOpenLv:"+BuildOpenLv

            +"    PresidentNum:"+PresidentNum

            +"    VicePresidentNum:"+VicePresidentNum

            +"    EliteNum:"+EliteNum

            +"    MemberNum:"+MemberNum
;
        }
    }
}

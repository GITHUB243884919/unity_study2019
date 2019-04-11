//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GuildBoss
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 怪物贴图
        /// </summary>
        public string Icon;

        /// <summary>
        /// 背景图
        /// </summary>
        public string MapId;

        /// <summary>
        /// 姓名
        /// </summary>
        public int GuildBossName;

        /// <summary>
        /// 开启公会等级
        /// </summary>
        public int OpenLv;

        /// <summary>
        /// 开启所需财富
        /// </summary>
        public int NeedWealth;

        /// <summary>
        /// 开启所需元宝
        /// </summary>
        public int NeedMoney;

        /// <summary>
        /// 血量
        /// </summary>
        public int Hp;

        /// <summary>
        /// 联盟贡献
        /// </summary>
        public int Contribution;

        /// <summary>
        /// 联盟经验
        /// </summary>
        public int GuildExp;

        /// <summary>
        /// 击杀掉落ID
        /// </summary>
        public int DropId;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GuildBoss:"
    
            +"    ID:"+ID

            +"    Icon:"+Icon

            +"    MapId:"+MapId

            +"    GuildBossName:"+GuildBossName

            +"    OpenLv:"+OpenLv

            +"    NeedWealth:"+NeedWealth

            +"    NeedMoney:"+NeedMoney

            +"    Hp:"+Hp

            +"    Contribution:"+Contribution

            +"    GuildExp:"+GuildExp

            +"    DropId:"+DropId
;
        }
    }
}

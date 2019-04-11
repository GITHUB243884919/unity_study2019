//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GuildContribute
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// ICON
        /// </summary>
        public string Icon;

        /// <summary>
        /// 贡献名称
        /// </summary>
        public int ContributeName;

        /// <summary>
        /// 增加帮会经验
        /// </summary>
        public int GuildExp;

        /// <summary>
        /// 增加帮会财富
        /// </summary>
        public int Asset;

        /// <summary>
        /// 增加个人贡献
        /// </summary>
        public int Contribution;

        /// <summary>
        /// 所需钻石
        /// </summary>
        public int Diamonds;

        /// <summary>
        /// 所需道具
        /// </summary>
        public int ItemId;

        /// <summary>
        /// 道具数量
        /// </summary>
        public int ItemNum;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GuildContribute:"
    
            +"    ID:"+ID

            +"    Icon:"+Icon

            +"    ContributeName:"+ContributeName

            +"    GuildExp:"+GuildExp

            +"    Asset:"+Asset

            +"    Contribution:"+Contribution

            +"    Diamonds:"+Diamonds

            +"    ItemId:"+ItemId

            +"    ItemNum:"+ItemNum
;
        }
    }
}

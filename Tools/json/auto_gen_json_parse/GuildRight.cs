//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class GuildRight
    {
        #region field

        /// <summary>
        /// id
        /// </summary>
        public int id;

        /// <summary>
        /// 成员类型
        /// 1：会长
        /// 2：副会长
        /// 3：精英
        /// 4：会员
        /// </summary>
        public int PersonType;

        /// <summary>
        /// 分配职位
        /// </summary>
        public int Assign;

        /// <summary>
        /// 踢出成员
        /// </summary>
        public int DeletePer;

        /// <summary>
        /// 允许加入
        /// </summary>
        public int AddPer;

        /// <summary>
        /// 钻石开启副本boss
        /// </summary>
        public int BossDiamond;

        /// <summary>
        /// 解散帮会
        /// </summary>
        public int Dissolve;

        /// <summary>
        /// 帮会财富开启副本boss
        /// </summary>
        public int BossAsset;

        #endregion
        
        public override string ToString ()
        {
            return 
            " GuildRight:"
    
            +"    id:"+id

            +"    PersonType:"+PersonType

            +"    Assign:"+Assign

            +"    DeletePer:"+DeletePer

            +"    AddPer:"+AddPer

            +"    BossDiamond:"+BossDiamond

            +"    Dissolve:"+Dissolve

            +"    BossAsset:"+BossAsset
;
        }
    }
}

//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class CabinetReward
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 奖励类型
        /// </summary>
        public int RewardType;

        /// <summary>
        /// 名次
        /// </summary>
        public int Ranking;

        /// <summary>
        /// 道具ID1
        /// </summary>
        public int ItemId1;

        /// <summary>
        /// 数量1
        /// </summary>
        public int Number1;

        /// <summary>
        /// 道具ID2
        /// </summary>
        public int ItemId2;

        /// <summary>
        /// 数量2
        /// </summary>
        public int Number2;

        #endregion
        
        public override string ToString ()
        {
            return 
            " CabinetReward:"
    
            +"    ID:"+ID

            +"    RewardType:"+RewardType

            +"    Ranking:"+Ranking

            +"    ItemId1:"+ItemId1

            +"    Number1:"+Number1

            +"    ItemId2:"+ItemId2

            +"    Number2:"+Number2
;
        }
    }
}

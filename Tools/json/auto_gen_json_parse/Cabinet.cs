//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Cabinet
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 事件等级
        /// </summary>
        public int CabinetLv;

        /// <summary>
        /// 事件等级描述
        /// </summary>
        public int CabinetLvDes;

        /// <summary>
        /// 概率
        /// </summary>
        public int Probability;

        /// <summary>
        /// 事件名称
        /// </summary>
        public int CabinetName;

        /// <summary>
        /// 事件描述
        /// </summary>
        public int CabinetDes;

        /// <summary>
        /// 对应所需属性类型
        /// 1：力量
        /// 2：智力
        /// 3：政治
        /// 4：魅力
        /// 5：总属性
        /// </summary>
        public int CabinetType;

        /// <summary>
        /// 所需属性描述
        /// </summary>
        public int CabinetTypeDes;

        /// <summary>
        /// 对应的底图
        /// </summary>
        public string CabinetMap;

        /// <summary>
        /// 对应称号ID
        /// </summary>
        public int Title;

        /// <summary>
        /// 对应的奖励类型
        /// </summary>
        public int RewardId;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Cabinet:"
    
            +"    ID:"+ID

            +"    CabinetLv:"+CabinetLv

            +"    CabinetLvDes:"+CabinetLvDes

            +"    Probability:"+Probability

            +"    CabinetName:"+CabinetName

            +"    CabinetDes:"+CabinetDes

            +"    CabinetType:"+CabinetType

            +"    CabinetTypeDes:"+CabinetTypeDes

            +"    CabinetMap:"+CabinetMap

            +"    Title:"+Title

            +"    RewardId:"+RewardId
;
        }
    }
}

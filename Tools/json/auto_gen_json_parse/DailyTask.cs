//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class DailyTask
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 描述
        /// </summary>
        public int Explain;

        /// <summary>
        /// Icon
        /// </summary>
        public int Icon;

        /// <summary>
        /// 任务类型
        /// 5：关卡胜利次数
        /// 6：经商产出次数
        /// 7：经营农产次数
        /// 8：招募士兵次数
        /// 9：处理政务次数
        /// 11：衙门出战次数
        /// 12：书院学习次数
        /// 13：教育犯人次数
        /// 15：随机传唤次数
        /// 16：累计寻访次数
        /// 20：宴会赴宴次数
        /// 21：通商胜利次数
        /// 26：子嗣培养次数
        /// 27：排行榜膜拜次数
        /// 28：皇宫请安次数
        /// 37：联盟建设次数
        /// 39：领取月卡
        /// 40：领取年卡
        /// 41：讨伐次数
        /// 42：门客升级
        /// 43：强化书籍
        /// 
        /// 
        /// </summary>
        public int TaskType;

        /// <summary>
        /// 需求数量
        /// </summary>
        public int NeedNum;

        /// <summary>
        /// 获得的积分数量
        /// </summary>
        public int Number;

        /// <summary>
        /// 1：EXP
        /// 2：钻石
        /// </summary>
        public int RewardType;

        /// <summary>
        /// 玩家经验
        /// </summary>
        public int PlayerExp;

        /// <summary>
        /// 功能跳转ID
        /// </summary>
        public int FunctionId;

        #endregion
        
        public override string ToString ()
        {
            return 
            " DailyTask:"
    
            +"    ID:"+ID

            +"    Explain:"+Explain

            +"    Icon:"+Icon

            +"    TaskType:"+TaskType

            +"    NeedNum:"+NeedNum

            +"    Number:"+Number

            +"    RewardType:"+RewardType

            +"    PlayerExp:"+PlayerExp

            +"    FunctionId:"+FunctionId
;
        }
    }
}

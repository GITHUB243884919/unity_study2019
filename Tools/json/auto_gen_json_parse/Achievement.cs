//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Achievement
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 是否是起始成就
        /// 1：是
        /// 0：不是
        /// </summary>
        public int IsStart;

        /// <summary>
        /// 描述
        /// </summary>
        public int Describe;

        /// <summary>
        /// 下一个成就ID
        /// </summary>
        public int NextId;

        /// <summary>
        /// 完成条件
        /// 1：登录天数
        /// 2：官品等级
        /// 3：累计势力
        /// 4：累计门客数量
        /// 5：关卡胜利次数
        /// 6：经商产出次数
        /// 7：经营农产次数
        /// 8：招募士兵次数
        /// 9：处理政务次数
        /// 10：VIP等级
        /// 11：衙门出战次数
        /// 12：书院学习次数
        /// 13：教育犯人次数
        /// 14：累计红颜数量
        /// 15：随机传唤次数
        /// 16：累计寻访次数
        /// 17：累计子嗣数量
        /// 18：累计联姻次数
        /// 19：世界BOSS击杀次数
        /// 20：宴会赴宴次数
        /// 21：通商胜利次数
        /// 37：联盟建设次数
        /// 
        /// </summary>
        public int Type;

        /// <summary>
        /// 完成数量
        /// </summary>
        public int Number;

        /// <summary>
        /// ID
        /// </summary>
        public int ItemId1;

        /// <summary>
        /// 数量
        /// </summary>
        public int Number1;

        /// <summary>
        /// ID
        /// </summary>
        public int ItemId2;

        /// <summary>
        /// 数量
        /// </summary>
        public int Number2;

        /// <summary>
        /// 组ID
        /// </summary>
        public int GroupId;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Achievement:"
    
            +"    ID:"+ID

            +"    IsStart:"+IsStart

            +"    Describe:"+Describe

            +"    NextId:"+NextId

            +"    Type:"+Type

            +"    Number:"+Number

            +"    ItemId1:"+ItemId1

            +"    Number1:"+Number1

            +"    ItemId2:"+ItemId2

            +"    Number2:"+Number2

            +"    GroupId:"+GroupId
;
        }
    }
}

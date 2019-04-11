//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class MainTask
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 名称
        /// </summary>
        public int TaskName;

        /// <summary>
        /// 描述
        /// </summary>
        public int TaskDes;

        /// <summary>
        /// 下一级
        /// </summary>
        public int NextId;

        /// <summary>
        /// 任务类型
        /// 1：登录天数
        /// 2：官品等级
        /// 3：累计影响力
        /// 4：累计门客数量
        /// 5：关卡胜利次数
        /// 6：经商产出次数
        /// 7：经营农产次数
        /// 8：招募士兵次数
        /// 9：处理政务次数
        /// 10：VIP等级
        /// 11：竞技场出战次数
        /// 12：学院学习次数
        /// 13：教育犯人次数
        /// 14：累计夫人数量
        /// 15：随机传唤次数
        /// 16：累计寻访次数
        /// 17：累计子嗣数量
        /// 18：累计结婚次数
        /// 19：世界BOSS击杀次数
        /// 20：宴会赴宴次数
        /// 21：通商胜利次数
        /// 22：Value1的英雄等级到Value2
        /// 23：Value1门客等级Value2个数
        /// 24：使用value1道具value2次数
        /// 25：宠幸夫人次数
        /// 26：子嗣培养次数
        /// 27：排行榜膜拜次数
        /// 28：总统府拜访次数
        /// 29：夫人送礼物次数
        /// 30：扩展子嗣席位
        /// 31：扩展学院席位
        /// 32：商城购买道具次数
        /// 33：英雄技能升级次数
        /// 34：夫人技能升级次数
        /// 35：value1的英雄爵位到达value2
        /// 36：成功加入联盟
        /// 37：联盟建设次数
        /// 38：联盟兑换次数
        /// 39：领取月卡
        /// 40：领取年卡
        /// 41：讨伐次数
        /// 42：门客升级
        /// 43：强化书籍
        /// </summary>
        public int TaskType;

        /// <summary>
        /// 参数1
        /// </summary>
        public int Value1;

        /// <summary>
        /// 参数2
        /// </summary>
        public int Value2;

        /// <summary>
        /// 道具类型
        /// 1：道具
        /// 2：英雄
        /// </summary>
        public int ItemType;

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

        /// <summary>
        /// 道具ID3
        /// </summary>
        public int ItemId3;

        /// <summary>
        /// 数量3
        /// </summary>
        public int Number3;

        #endregion
        
        public override string ToString ()
        {
            return 
            " MainTask:"
    
            +"    ID:"+ID

            +"    TaskName:"+TaskName

            +"    TaskDes:"+TaskDes

            +"    NextId:"+NextId

            +"    TaskType:"+TaskType

            +"    Value1:"+Value1

            +"    Value2:"+Value2

            +"    ItemType:"+ItemType

            +"    ItemId1:"+ItemId1

            +"    Number1:"+Number1

            +"    ItemId2:"+ItemId2

            +"    Number2:"+Number2

            +"    ItemId3:"+ItemId3

            +"    Number3:"+Number3
;
        }
    }
}

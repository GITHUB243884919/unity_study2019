//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class ChildDegree
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 身份类别
        /// 1：高中
        /// 2：学士
        /// 3：双学位学士
        /// 4：普通院校硕士
        /// 5：知名院校硕士
        /// 6：博士
        /// 7：知名院校博士
        /// </summary>
        public int DegreeType;

        /// <summary>
        /// 学位描述
        /// </summary>
        public int DegreeDes;

        /// <summary>
        /// 学位文字图片资源
        /// </summary>
        public string DegreePic;

        /// <summary>
        /// 属性下限
        /// </summary>
        public int AttrMin;

        /// <summary>
        /// 属性上限
        /// </summary>
        public int AttrMax;

        /// <summary>
        /// 男孩衣服
        /// </summary>
        public string Clothes1;

        /// <summary>
        /// 女孩衣服
        /// </summary>
        public string Clothes2;

        /// <summary>
        /// 结婚钻石消耗
        /// </summary>
        public int DiamondsCost;

        /// <summary>
        /// 消耗道具ID
        /// </summary>
        public int ItemId;

        /// <summary>
        /// 道具数量
        /// </summary>
        public int Number;

        /// <summary>
        /// 奖励道具1
        /// </summary>
        public int RewardItemId1;

        /// <summary>
        /// 数量1
        /// </summary>
        public int Num1;

        /// <summary>
        /// 奖励道具2
        /// </summary>
        public int RewardItemId2;

        /// <summary>
        /// 数量2
        /// </summary>
        public int Num2;

        #endregion
        
        public override string ToString ()
        {
            return 
            " ChildDegree:"
    
            +"    ID:"+ID

            +"    DegreeType:"+DegreeType

            +"    DegreeDes:"+DegreeDes

            +"    DegreePic:"+DegreePic

            +"    AttrMin:"+AttrMin

            +"    AttrMax:"+AttrMax

            +"    Clothes1:"+Clothes1

            +"    Clothes2:"+Clothes2

            +"    DiamondsCost:"+DiamondsCost

            +"    ItemId:"+ItemId

            +"    Number:"+Number

            +"    RewardItemId1:"+RewardItemId1

            +"    Num1:"+Num1

            +"    RewardItemId2:"+RewardItemId2

            +"    Num2:"+Num2
;
        }
    }
}

//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Fb
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 类型
        /// 1：士兵
        /// 2：BOSS
        /// </summary>
        public int Type;

        /// <summary>
        /// 下一个ID
        /// </summary>
        public int NextId;

        /// <summary>
        /// 名称
        /// </summary>
        public int Name;

        /// <summary>
        /// 背景图
        /// </summary>
        public string Background;

        /// <summary>
        /// Boss图片
        /// </summary>
        public string Icon;

        /// <summary>
        /// 对话
        /// 对应Language
        /// </summary>
        public int Talk;

        /// <summary>
        /// 战斗力
        /// </summary>
        public int Power;

        /// <summary>
        /// 副本分数
        /// </summary>
        public int Fraction;

        /// <summary>
        /// 副本积分
        /// </summary>
        public int Score;

        /// <summary>
        /// 掉落ID
        /// </summary>
        public int DropId;

        /// <summary>
        /// 黄金系数下限
        /// </summary>
        public int ValueMin;

        /// <summary>
        /// 黄金系数上限
        /// </summary>
        public int ValueMax;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Fb:"
    
            +"    ID:"+ID

            +"    Type:"+Type

            +"    NextId:"+NextId

            +"    Name:"+Name

            +"    Background:"+Background

            +"    Icon:"+Icon

            +"    Talk:"+Talk

            +"    Power:"+Power

            +"    Fraction:"+Fraction

            +"    Score:"+Score

            +"    DropId:"+DropId

            +"    ValueMin:"+ValueMin

            +"    ValueMax:"+ValueMax
;
        }
    }
}

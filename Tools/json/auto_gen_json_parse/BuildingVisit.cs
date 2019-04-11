//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class BuildingVisit
    {
        #region field

        /// <summary>
        /// 事件ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 建筑物ID
        /// </summary>
        public int BuildingID;

        /// <summary>
        /// NPC类型
        /// 0：非红颜
        /// 1：红颜
        /// </summary>
        public int NpcType;

        /// <summary>
        /// NpcId
        /// </summary>
        public int NpcId;

        /// <summary>
        /// NPC名称
        /// </summary>
        public int NpcName;

        /// <summary>
        /// NPC称号
        /// </summary>
        public int NpcTitle;

        /// <summary>
        /// NPC图片索引
        /// </summary>
        public string NpcIcon;

        /// <summary>
        /// NPC描述
        /// </summary>
        public int NpcDes;

        /// <summary>
        /// 好感度成长
        /// </summary>
        public int AttractUP;

        /// <summary>
        /// NPC文字描述1
        /// </summary>
        public int NpcText1;

        /// <summary>
        /// NPC文字描述2
        /// </summary>
        public int NpcText2;

        /// <summary>
        /// NPC文字描述3
        /// </summary>
        public int NpcText3;

        /// <summary>
        /// NPC文字描述4
        /// </summary>
        public int NpcText4;

        /// <summary>
        /// NPC文字描述5
        /// </summary>
        public int NpcText5;

        /// <summary>
        /// 解锁途径描述
        /// </summary>
        public int Channel;

        /// <summary>
        /// 结婚后文字描述
        /// </summary>
        public int MarryText;

        /// <summary>
        /// 掉落道具ID
        /// </summary>
        public int DropItemId;

        /// <summary>
        /// 数量
        /// -1：表示需要读取当前经营的数据
        /// </summary>
        public int Number;

        /// <summary>
        /// 随机数下限
        /// </summary>
        public string RandomMax;

        /// <summary>
        /// 随机数上限
        /// </summary>
        public string RandomMin;

        /// <summary>
        /// 增加好感度值
        /// </summary>
        public int Intimacy;

        #endregion
        
        public override string ToString ()
        {
            return 
            " BuildingVisit:"
    
            +"    ID:"+ID

            +"    BuildingID:"+BuildingID

            +"    NpcType:"+NpcType

            +"    NpcId:"+NpcId

            +"    NpcName:"+NpcName

            +"    NpcTitle:"+NpcTitle

            +"    NpcIcon:"+NpcIcon

            +"    NpcDes:"+NpcDes

            +"    AttractUP:"+AttractUP

            +"    NpcText1:"+NpcText1

            +"    NpcText2:"+NpcText2

            +"    NpcText3:"+NpcText3

            +"    NpcText4:"+NpcText4

            +"    NpcText5:"+NpcText5

            +"    Channel:"+Channel

            +"    MarryText:"+MarryText

            +"    DropItemId:"+DropItemId

            +"    Number:"+Number

            +"    RandomMax:"+RandomMax

            +"    RandomMin:"+RandomMin

            +"    Intimacy:"+Intimacy
;
        }
    }
}

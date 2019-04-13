//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class NewPlayer
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 1：有框引导
        /// 2：无框引导
        /// </summary>
        public int Type;

        /// <summary>
        /// 是否是同一系列引导
        /// </summary>
        public int IsSame;

        /// <summary>
        /// UI名称
        /// </summary>
        public string UiName;

        /// <summary>
        /// 主界面横坐标
        /// </summary>
        public float SenceX;

        /// <summary>
        /// 框参数1
        /// </summary>
        public float ItemX;

        /// <summary>
        /// 框参数2
        /// </summary>
        public float ItemY;

        /// <summary>
        /// 框参数3
        /// </summary>
        public int ItemSize1;

        /// <summary>
        /// 框参数4
        /// </summary>
        public int ItemSize2;

        /// <summary>
        /// 0.0
        /// </summary>
        public float TopX;

        /// <summary>
        /// 0.0
        /// </summary>
        public float TopY;

        /// <summary>
        /// 0.0
        /// </summary>
        public int TopSize1;

        /// <summary>
        /// 0.0
        /// </summary>
        public int TopSize2;

        /// <summary>
        /// 0.0
        /// </summary>
        public float BottomX;

        /// <summary>
        /// 0.0
        /// </summary>
        public float BottomY;

        /// <summary>
        /// 0.0
        /// </summary>
        public int BottomSize1;

        /// <summary>
        /// 0.0
        /// </summary>
        public int BottomSize2;

        /// <summary>
        /// 0.0
        /// </summary>
        public float LeftX;

        /// <summary>
        /// 0.0
        /// </summary>
        public float LeftY;

        /// <summary>
        /// 0.0
        /// </summary>
        public int LeftSize1;

        /// <summary>
        /// 0.0
        /// </summary>
        public int LeftSize2;

        /// <summary>
        /// 0.0
        /// </summary>
        public float RightX;

        /// <summary>
        /// 0.0
        /// </summary>
        public float RightY;

        /// <summary>
        /// 0.0
        /// </summary>
        public int RightSize1;

        /// <summary>
        /// 0.0
        /// </summary>
        public int RightSize2;

        /// <summary>
        /// 底图索引
        /// </summary>
        public string PicDes;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// 
        /// </summary>
        public int DesType1;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon1;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue1;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// </summary>
        public int DesType2;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon2;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue2;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// </summary>
        public int DesType3;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon3;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue3;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// </summary>
        public int DesType4;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon4;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue4;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// </summary>
        public int DesType5;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon5;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue5;

        /// <summary>
        /// 显示类型：
        /// 1：左边显示人物
        /// 2：右边显示人物
        /// </summary>
        public int DesType6;

        /// <summary>
        /// NPC人物形象索引
        /// 1：默认为玩家形象（玩家信息展示界面）2：界面不显示任何形象
        /// </summary>
        public string NpcIcon6;

        /// <summary>
        /// 对话
        /// </summary>
        public int Dialogue6;

        #endregion
        
        public override string ToString ()
        {
            return 
            " NewPlayer:"
    
            +"    ID:"+ID

            +"    Type:"+Type

            +"    IsSame:"+IsSame

            +"    UiName:"+UiName

            +"    SenceX:"+SenceX

            +"    ItemX:"+ItemX

            +"    ItemY:"+ItemY

            +"    ItemSize1:"+ItemSize1

            +"    ItemSize2:"+ItemSize2

            +"    TopX:"+TopX

            +"    TopY:"+TopY

            +"    TopSize1:"+TopSize1

            +"    TopSize2:"+TopSize2

            +"    BottomX:"+BottomX

            +"    BottomY:"+BottomY

            +"    BottomSize1:"+BottomSize1

            +"    BottomSize2:"+BottomSize2

            +"    LeftX:"+LeftX

            +"    LeftY:"+LeftY

            +"    LeftSize1:"+LeftSize1

            +"    LeftSize2:"+LeftSize2

            +"    RightX:"+RightX

            +"    RightY:"+RightY

            +"    RightSize1:"+RightSize1

            +"    RightSize2:"+RightSize2

            +"    PicDes:"+PicDes

            +"    DesType1:"+DesType1

            +"    NpcIcon1:"+NpcIcon1

            +"    Dialogue1:"+Dialogue1

            +"    DesType2:"+DesType2

            +"    NpcIcon2:"+NpcIcon2

            +"    Dialogue2:"+Dialogue2

            +"    DesType3:"+DesType3

            +"    NpcIcon3:"+NpcIcon3

            +"    Dialogue3:"+Dialogue3

            +"    DesType4:"+DesType4

            +"    NpcIcon4:"+NpcIcon4

            +"    Dialogue4:"+Dialogue4

            +"    DesType5:"+DesType5

            +"    NpcIcon5:"+NpcIcon5

            +"    Dialogue5:"+Dialogue5

            +"    DesType6:"+DesType6

            +"    NpcIcon6:"+NpcIcon6

            +"    Dialogue6:"+Dialogue6
;
        }
    }
}

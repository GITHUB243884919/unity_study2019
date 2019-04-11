//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class ActivityAccount
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 排序序号
        /// 能领的奖励排最前面
        /// 序号越大越排前面
        /// 二级按照ID从小到大来排
        /// </summary>
        public int Index;

        /// <summary>
        /// 是否开放
        /// </summary>
        public int IsOpen;

        /// <summary>
        /// 按钮图片
        /// </summary>
        public string PicType;

        /// <summary>
        /// 活动名称
        /// </summary>
        public int Name;

        /// <summary>
        /// 活动UI
        /// </summary>
        public string ActivityUi;

        #endregion
        
        public override string ToString ()
        {
            return 
            " ActivityAccount:"
    
            +"    ID:"+ID

            +"    Index:"+Index

            +"    IsOpen:"+IsOpen

            +"    PicType:"+PicType

            +"    Name:"+Name

            +"    ActivityUi:"+ActivityUi
;
        }
    }
}

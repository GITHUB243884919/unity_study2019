//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Mission
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 底图
        /// </summary>
        public string Picture;

        /// <summary>
        /// 任务描述
        /// </summary>
        public int MissionDes;

        /// <summary>
        /// 选项描述1
        /// </summary>
        public int OptionDes1;

        /// <summary>
        /// 选项描述2
        /// </summary>
        public int OptionDes2;

        /// <summary>
        /// 道具Id
        /// </summary>
        public int ItemId;

        /// <summary>
        /// 下限
        /// </summary>
        public int RandomMin;

        /// <summary>
        /// 上限
        /// </summary>
        public int RandomMax;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Mission:"
    
            +"    ID:"+ID

            +"    Picture:"+Picture

            +"    MissionDes:"+MissionDes

            +"    OptionDes1:"+OptionDes1

            +"    OptionDes2:"+OptionDes2

            +"    ItemId:"+ItemId

            +"    RandomMin:"+RandomMin

            +"    RandomMax:"+RandomMax
;
        }
    }
}

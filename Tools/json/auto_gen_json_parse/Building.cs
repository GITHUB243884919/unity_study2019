//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Building
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 建筑物ID
        /// </summary>
        public int BuildingID;

        /// <summary>
        /// 建筑物名称
        /// </summary>
        public int BuildingName;

        /// <summary>
        /// 建筑物贴图
        /// </summary>
        public string BuildIcon;

        /// <summary>
        /// 建筑物名称索引
        /// </summary>
        public string BuildNameIcon;

        /// <summary>
        /// 底图
        /// </summary>
        public string BackIcon;

        /// <summary>
        /// 都不碰上的描述
        /// </summary>
        public int MissDes;

        /// <summary>
        /// 玩家未持有红颜时是否可随机到
        /// 0：不可
        /// 1：可
        /// </summary>
        public int IsRandom;

        /// <summary>
        /// 建筑物随机概率
        /// </summary>
        public int BuildingPro;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Building:"
    
            +"    ID:"+ID

            +"    BuildingID:"+BuildingID

            +"    BuildingName:"+BuildingName

            +"    BuildIcon:"+BuildIcon

            +"    BuildNameIcon:"+BuildNameIcon

            +"    BackIcon:"+BackIcon

            +"    MissDes:"+MissDes

            +"    IsRandom:"+IsRandom

            +"    BuildingPro:"+BuildingPro
;
        }
    }
}

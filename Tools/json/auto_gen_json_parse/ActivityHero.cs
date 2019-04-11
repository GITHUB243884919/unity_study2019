//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class ActivityHero
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 任务ID
        /// </summary>
        public int ActivityID;

        /// <summary>
        /// 任务ID
        /// ActivityHeroList->ID
        /// </summary>
        public string TaskId;

        /// <summary>
        /// 背景
        /// </summary>
        public string Bg;

        /// <summary>
        /// 图集路径
        /// </summary>
        public string AtlasUrl;

        #endregion
        
        public override string ToString ()
        {
            return 
            " ActivityHero:"
    
            +"    ID:"+ID

            +"    ActivityID:"+ActivityID

            +"    TaskId:"+TaskId

            +"    Bg:"+Bg

            +"    AtlasUrl:"+AtlasUrl
;
        }
    }
}

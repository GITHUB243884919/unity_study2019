//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class ActivityRechargeQuota
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
        /// 任务类型ID
        /// ActivityTask->TaskType
        /// </summary>
        public int TypeID;

        /// <summary>
        /// 任务ID
        /// ActivityTask->ID
        /// </summary>
        public string TaskId;

        #endregion
        
        public override string ToString ()
        {
            return 
            " ActivityRechargeQuota:"
    
            +"    ID:"+ID

            +"    ActivityID:"+ActivityID

            +"    TypeID:"+TypeID

            +"    TaskId:"+TaskId
;
        }
    }
}

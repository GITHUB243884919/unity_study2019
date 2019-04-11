//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class help
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 功能ID
        /// </summary>
        public int SystemId;

        /// <summary>
        /// 规则说明
        /// </summary>
        public int Text;

        #endregion
        
        public override string ToString ()
        {
            return 
            " help:"
    
            +"    ID:"+ID

            +"    SystemId:"+SystemId

            +"    Text:"+Text
;
        }
    }
}

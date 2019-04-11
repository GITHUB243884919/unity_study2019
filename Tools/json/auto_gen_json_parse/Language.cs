//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Language
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 中文
        /// 1：[00FF0000]绿色[-]
        /// 2：文本{0}文本{1}文本{2}
        /// 3：\n
        /// </summary>
        public string CN;

        /// <summary>
        /// 英文
        /// </summary>
        public string EN;

        /// <summary>
        /// 英文
        /// </summary>
        public string EN;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Language:"
    
            +"    ID:"+ID

            +"    CN:"+CN

            +"    EN:"+EN

            +"    EN:"+EN
;
        }
    }
}

//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class ChildLvUp
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 等级下限
        /// </summary>
        public int LvMin;

        /// <summary>
        /// 等级上限
        /// </summary>
        public int LvMax;

        /// <summary>
        /// 所需经验
        /// </summary>
        public int Exp;

        #endregion
        
        public override string ToString ()
        {
            return 
            " ChildLvUp:"
    
            +"    ID:"+ID

            +"    LvMin:"+LvMin

            +"    LvMax:"+LvMax

            +"    Exp:"+Exp
;
        }
    }
}

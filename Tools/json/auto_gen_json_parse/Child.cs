//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Child
    {
        #region field

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 等级
        /// </summary>
        public int Lv;

        /// <summary>
        /// 性别
        /// 1：男
        /// 2：女
        /// </summary>
        public int Sex;

        /// <summary>
        /// 婴儿半身像
        /// </summary>
        public string BabyBust;

        /// <summary>
        /// 小孩半身像
        /// </summary>
        public string ChildBust;

        /// <summary>
        /// 成年半身像
        /// </summary>
        public string AdultBust;

        /// <summary>
        /// 对应音频文件1
        /// </summary>
        public int Music1;

        /// <summary>
        /// 对应音频文件2
        /// </summary>
        public int Music2;

        /// <summary>
        /// 对应音频文件3
        /// </summary>
        public int Music3;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Child:"
    
            +"    ID:"+ID

            +"    Lv:"+Lv

            +"    Sex:"+Sex

            +"    BabyBust:"+BabyBust

            +"    ChildBust:"+ChildBust

            +"    AdultBust:"+AdultBust

            +"    Music1:"+Music1

            +"    Music2:"+Music2

            +"    Music3:"+Music3
;
        }
    }
}

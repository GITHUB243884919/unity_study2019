//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Item
    {
        #region field

        /// <summary>
        /// 道具ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 道具名称字符串ID（关联文字表）
        /// </summary>
        public int Name;

        /// <summary>
        /// 道具说明字符串ID（关联文字表）
        /// </summary>
        public int Explain1;

        /// <summary>
        /// 获取途径描述
        /// </summary>
        public int Explain2;

        /// <summary>
        /// 道具品级（颜色：白1，绿2，蓝3，紫4，橙5，红6）
        /// </summary>
        public int Quality;

        /// <summary>
        /// 是否可使用
        /// 0：无
        /// 1：作用于指定门客
        /// 2：作用于指定红颜
        /// 3：作用于玩家
        /// 4：随机门客
        /// 5：随机红颜
        /// 101：改名卡
        /// </summary>
        public int IsUse;

        /// <summary>
        /// 可否叠加 （不可以0，可以1）
        /// </summary>
        public int Superposition;

        /// <summary>
        /// 。
        /// 0：资源（不显示在背包内）
        /// 1：道具
        /// 2：合成
        /// 3：称号
        /// 4：皮肤
        /// </summary>
        public int Type;

        /// <summary>
        /// 对应类型ID
        /// 1：使用后走掉落逻辑
        /// 2：随机属性类型
        /// 3：固定属性类型
        /// 4：指定获得value1属性value2，value3的值
        /// </summary>
        public int ClassID;

        /// <summary>
        /// 附属值1
        /// </summary>
        public int ClassValue1;

        /// <summary>
        /// 附属值2
        /// </summary>
        public int ClassValue2;

        /// <summary>
        /// 附属值3
        /// </summary>
        public int ClassValue3;

        /// <summary>
        /// 合成数量
        /// </summary>
        public int Number;

        /// <summary>
        /// 合成的具体ID
        /// </summary>
        public int FormulaId;

        /// <summary>
        /// 对应DropID
        /// </summary>
        public int DropId;

        /// <summary>
        /// 时间类型
        /// 0：无
        /// 1：时间段
        /// </summary>
        public int TimeType;

        /// <summary>
        /// 开始年
        /// 
        /// </summary>
        public int StartYear;

        /// <summary>
        /// 开始月
        /// 
        /// </summary>
        public int StartMonth;

        /// <summary>
        /// 开始日
        /// 
        /// </summary>
        public int StartDay;

        /// <summary>
        /// 开始小时
        /// 
        /// </summary>
        public int StartHour;

        /// <summary>
        /// 结束年
        /// 
        /// </summary>
        public int EndYear;

        /// <summary>
        /// 结束月
        /// 
        /// </summary>
        public int EndMonth;

        /// <summary>
        /// 结束日
        /// 
        /// </summary>
        public int EndDay;

        /// <summary>
        /// 结束小时
        /// 
        /// </summary>
        public int EndHour;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Item:"
    
            +"    ID:"+ID

            +"    Name:"+Name

            +"    Explain1:"+Explain1

            +"    Explain2:"+Explain2

            +"    Icon:"+Icon

            +"    Quality:"+Quality

            +"    IsUse:"+IsUse

            +"    Superposition:"+Superposition

            +"    Type:"+Type

            +"    ClassID:"+ClassID

            +"    ClassValue1:"+ClassValue1

            +"    ClassValue2:"+ClassValue2

            +"    ClassValue3:"+ClassValue3

            +"    Number:"+Number

            +"    FormulaId:"+FormulaId

            +"    DropId:"+DropId

            +"    TimeType:"+TimeType

            +"    StartYear:"+StartYear

            +"    StartMonth:"+StartMonth

            +"    StartDay:"+StartDay

            +"    StartHour:"+StartHour

            +"    EndYear:"+EndYear

            +"    EndMonth:"+EndMonth

            +"    EndDay:"+EndDay

            +"    EndHour:"+EndHour
;
        }
    }
}

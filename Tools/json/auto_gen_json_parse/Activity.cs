//auto gen code by fanzhengyong

using UFrame.Json;
namespace Game.Json.Config
{
    public class Activity
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
        /// 活动类型
        /// 1：限时奖励(ActivityLimitReward)
        /// 2：英雄兑换
        /// （ActivityHero）
        /// 3：累计充值额度(ActivityRechargeQuota)
        /// 4：累计充值天数(ActivityRechargeDay)
        /// 5：个人冲榜(ActivityList)
        /// 6：商城
        /// </summary>
        public int ActivityType;

        /// <summary>
        /// 对应ID
        /// 对应各活动类型表中的任务ID
        /// </summary>
        public int ExcelId;

        /// <summary>
        /// 活动UI
        /// </summary>
        public string ActivityUi;

        /// <summary>
        /// 活动名称
        /// </summary>
        public int Name;

        /// <summary>
        /// 活动说明（上限200字）
        /// </summary>
        public int Describe;

        /// <summary>
        /// 开启限制类型
        /// 0 时间段
        /// 1 开服天数段
        /// 5 永久
        /// 6 周循环
        /// 7 N周循环
        /// </summary>
        public int TimeLimitType;

        /// <summary>
        /// 开始年
        /// 只在时间段选项生效
        /// </summary>
        public int StartYear;

        /// <summary>
        /// 开始月
        /// 只在时间段选项生效
        /// </summary>
        public int StartMonth;

        /// <summary>
        /// 开始日
        /// 只在时间段选项生效
        /// 
        /// 周循环时为周几开启
        /// 
        /// N周循环时，填第几周开启
        /// </summary>
        public int StartDay;

        /// <summary>
        /// 开始小时
        /// 只在时间段选项生效
        /// </summary>
        public int StartHour;

        /// <summary>
        /// 结束年
        /// 只在时间段选项生效
        /// </summary>
        public int EndYear;

        /// <summary>
        /// 结束月
        /// 只在时间段选项生效
        /// </summary>
        public int EndMonth;

        /// <summary>
        /// 结束日
        /// 只在时间段选项生效
        ///  
        /// N周循环时，填几周循环一次
        /// </summary>
        public int EndDay;

        /// <summary>
        /// 结束小时
        /// 只在时间段选项生效
        /// </summary>
        public int EndHour;

        /// <summary>
        /// 开服天数开始
        /// 只在开服天数段选项生效
        /// 若是永久开或者周循环 4周循环，则填写的数字为开服后第几天开启。
        /// </summary>
        public int ServerDayStart;

        /// <summary>
        /// 开服天数结束
        /// 只在开服天数段选项生效
        /// </summary>
        public int ServerDayEnd;

        /// <summary>
        /// 周循环，N周循环，月循环时填写持续几天
        /// </summary>
        public int Continue;

        /// <summary>
        /// 重复类型
        /// 0 单次
        /// 1 每日重置
        /// 2 每周重置
        /// 3几天重置
        /// </summary>
        public int LoopType;

        /// <summary>
        /// 重复次数，只在限次重置时生效
        /// 若类型为3，则是几天一重置
        /// </summary>
        public int LoopValue;

        #endregion
        
        public override string ToString ()
        {
            return 
            " Activity:"
    
            +"    ID:"+ID

            +"    Index:"+Index

            +"    IsOpen:"+IsOpen

            +"    PicType:"+PicType

            +"    ActivityType:"+ActivityType

            +"    ExcelId:"+ExcelId

            +"    ActivityUi:"+ActivityUi

            +"    Name:"+Name

            +"    Describe:"+Describe

            +"    TimeLimitType:"+TimeLimitType

            +"    StartYear:"+StartYear

            +"    StartMonth:"+StartMonth

            +"    StartDay:"+StartDay

            +"    StartHour:"+StartHour

            +"    EndYear:"+EndYear

            +"    EndMonth:"+EndMonth

            +"    EndDay:"+EndDay

            +"    EndHour:"+EndHour

            +"    ServerDayStart:"+ServerDayStart

            +"    ServerDayEnd:"+ServerDayEnd

            +"    Continue:"+Continue

            +"    LoopType:"+LoopType

            +"    LoopValue:"+LoopValue
;
        }
    }
}

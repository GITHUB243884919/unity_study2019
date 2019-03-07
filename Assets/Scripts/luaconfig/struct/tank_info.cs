
//auto gen code
using UFrame.ToLua;
namespace Game.ToLua.Config
{
    public class tank_info
    {
        #region field

		/// <summary>
		/// id
		/// </summary>
		public int id;

		/// <summary>
		/// 类型编号
		/// </summary>
		public int tank_type;

		/// <summary>
		/// 资源路径
		/// </summary>
		public string res_path;

		/// <summary>
		/// 速度
		/// </summary>
		public double speed;

		/// <summary>
		/// 转向速度
		/// </summary>
		public double turn_speed;

		/// <summary>
		/// 探测距离
		/// </summary>
		public double detection_len;

        #endregion
        
        public override string ToString ()
		{
			return 
			" tank_info:"
	
    		+"	id:"+id

    		+"	tank_type:"+tank_type

    		+"	res_path:"+res_path

    		+"	speed:"+speed

    		+"	turn_speed:"+turn_speed

    		+"	detection_len:"+detection_len
;
		}
    }
}

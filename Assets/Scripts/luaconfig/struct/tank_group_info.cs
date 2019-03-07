
//auto gen code
using UFrame.ToLua;
namespace GameName.Lua.Config
{
    public class tank_group_info
    {
        #region field

		/// <summary>
		/// id
		/// </summary>
		public int id;

		/// <summary>
		/// 群编号
		/// </summary>
		public int tank_group_id;

		/// <summary>
		/// 坦克类型
		/// </summary>
		public int tank_type;

		/// <summary>
		/// 位置
		/// </summary>
		public ArrayData pos;

		/// <summary>
		/// 朝向
		/// </summary>
		public ArrayData dir;

        #endregion
        
        public override string ToString ()
		{
			return 
			" tank_group_info:"
	
    		+"	id:"+id

    		+"	tank_group_id:"+tank_group_id

    		+"	tank_type:"+tank_type

    		+"	pos:"+pos

    		+"	dir:"+dir
;
		}
    }
}

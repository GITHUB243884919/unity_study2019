
//auto gen code
using UFrame.ToLua;
namespace GameName.Lua.Config
{
    public class stage_info
    {
        #region field

		/// <summary>
		/// id
		/// </summary>
		public int id;

		/// <summary>
		/// 类型编号
		/// </summary>
		public int tank_group_id;

        #endregion
        
        public override string ToString ()
		{
			return 
			" stage_info:"
	
    		+"	id:"+id

    		+"	tank_group_id:"+tank_group_id
;
		}
    }
}

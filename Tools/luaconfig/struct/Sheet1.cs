
//auto gen code
using UFrame.ToLua;
namespace GameName.Lua.Config
{
    public class Sheet1
    {
        #region field

		/// <summary>
		/// id
		/// </summary>
		public int id;

		/// <summary>
		/// 字段1
		/// </summary>
		public string field1;

		/// <summary>
		/// 字段二
		/// </summary>
		public int field2;

		/// <summary>
		/// 字段三
		/// </summary>
		public ArrayData field3;

		/// <summary>
		/// 字段4
		/// </summary>
		public float field4;

		/// <summary>
		/// 字段5
		/// </summary>
		public double field5;

        #endregion
        
        public override string ToString ()
		{
			return 
			" Sheet1:"
	
    		+"	id:"+id

    		+"	field1:"+field1

    		+"	field2:"+field2

    		+"	field3:"+field3

    		+"	field4:"+field4

    		+"	field5:"+field5
;
		}
    }
}

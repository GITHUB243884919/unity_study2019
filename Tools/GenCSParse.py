import xdrlib, sys
import os
import xlrd
import codecs
import shutil

#import _PlistKit

dao_file_context = """using System;
using System.Collections.Generic;
using LuaInterface;
using UFrame.LUA;
using %s;

namespace %s
{
    public class $classname$Parse : LuaDaoBase <$classname$>
    {
        public override void AddPrimaryIndex ($classname$ model)
        {
            cachePrimary[model.%s.ToString ()] = model;
        }

        protected override string GetLuaFileName ()
        {
            return "%s";
        }

        protected override $classname$ ParseLuaTableData (string key, LuaTable tableData)
        {
            $classname$ data = new $classname$ ();
%s

            return data;
        }

%s

    }

}
"""

dao_cache_contect = """
		protected Dictionary<$Type$, List<$classname$>> cache_$paramname$;

		public Dictionary<$Type$, List<$classname$>> Cache_$paramname$ {
			get {
				return cache_$paramname$;
			}
		}
"""

dao_buildindex_context = """
		public override void BuildIndex ()
		{
			base.BuildIndex ();
			if (cacheList != null) {
%s
			}
		}
"""

dao_buildindex_sub_context = """
				cache_$paramname$ = new Dictionary<$Type$, List<$classname$>> ();
				foreach(var data in cacheList){
					if (!cache_$paramname$.ContainsKey (data.$paramname$)) {
						cache_$paramname$ [data.$paramname$] = new List<$classname$>();
					}
					cache_$paramname$ [data.$paramname$].Add (data);
				}
"""

dao_int_context = """
			data.%s = System.Convert.ToInt32(tableData["%s"]);"""

dao_float_context = """
			data.%s = System.Convert.ToSingle(tableData["%s"]);"""

dao_double_context = """
			data.%s = System.Convert.ToDouble(tableData["%s"]);"""

dao_string_context = """
			data.%s = tableData["%s"] as string;"""

dao_arr_context = """
			data.%s = new ArrayData (LuaTableToArrayParam (tableData["%s"] as LuaTable));"""

def Gen(dir, sheetName, name_row, type_row, col_num, charp_model_namespace, charp_dao_namespace, cs_file_copy_abs_path):
	lua_config_file_name = "config_" + sheetName
	className = sheetName + "Parse"

	file_context = dao_file_context.replace("$classname$", sheetName);

	#key_word_array = _PlistKit.GetDictKeyWords (sheetName);

	cache_tmp = "";
	build_index_sub_tmp = "";

	context = ""
	for i in range(0, col_num):
		var_name = str(name_row[i])
		if var_name.startswith("*"):
			continue;
		if len(var_name) <= 0:
			continue;
		if len(str(type_row[i])) <= 0:
			continue;

		if (type_row[i] == "int"):
			context += dao_int_context % (var_name, var_name)
		elif (type_row[i] == "float"):
			context += dao_float_context % (var_name, var_name)
		elif (type_row[i] == "double"):
			context += dao_double_context % (var_name, var_name)
		elif (type_row[i] == "string"):
			context += dao_string_context % (var_name, var_name)
		elif (type_row[i] == "array"):
			context += dao_arr_context % (var_name, var_name)
		else:
			print("!!!!----Ignore Type '" + type_row[i] + "'")
			continue;

		#if (var_name in key_word_array):
		#	cache_tmp += dao_cache_contect.replace ("$Type$", type_row[i]).replace ("$classname$", "Dict" + sheetName).replace ("$paramname$", var_name)
		#	build_index_sub_tmp += dao_buildindex_sub_context.replace ("$Type$", type_row[i]).replace ("$classname$", "Dict" + sheetName).replace ("$paramname$", var_name)

	help_param_code = "";
	#if len(key_word_array) > 0:
	#	help_param_code = cache_tmp + (dao_buildindex_context % (build_index_sub_tmp))

	final_result = file_context % (charp_model_namespace, charp_dao_namespace, name_row[0], lua_config_file_name, context, help_param_code)

	#if (os.path.exists("Dao") == False):
	#	os.mkdir("Dao")
	fp = codecs.open(dir + "/" + className + ".cs", "w", "utf_8")
	fp.write(final_result)
	fp.close()

	#fp = codecs.open(cs_file_copy_abs_path + "/Dao/" + className + ".cs", "w", "utf_8")
	#fp.write(final_result)
	#fp.close()

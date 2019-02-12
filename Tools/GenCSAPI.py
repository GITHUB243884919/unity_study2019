import xdrlib, sys
import os
import xlrd
import codecs
import shutil

blo_file_context = """
//Auto Generate
using System;
using System.Collections.Generic;

namespace %s
{
    public partial class $classname$API
    {
        public static $classname$ Get$classname$ (string dictId)
		{
			return LuaConfigManager.Instance.%s.GetById (dictId);
		}

%s

    }

}
"""

blo_getlist_context = """
		public static $classname$ GetDataBy_$paramname$ (List<$classname$> list, $Type$ target)
		{
			foreach (var item in list){
				if (item.$paramname$ == target) {
					return item;
				}
			}
			return null;
		}

		public static List<$classname$> GetDataListBy_$paramname$ (List<$classname$> list, $Type$ target)
		{
			List<$classname$> res = new List<$classname$> ();
			foreach (var item in list){
				if (item.$paramname$ == target) {
					res.Add (item);
				}
			}
			return res;
		}
"""

def Gen(dir, sheet_name, name_row, type_row, col_num, charp_model_namespace, charp_blo_namespace, apiFiles):

	class_api_name = sheet_name + "API"

	filecontext = blo_file_context.replace("$classname$", sheet_name);

	get_list_tmp = "";

	for i in range(0, col_num):
		var_name = str(name_row[i])
		if var_name.startswith("*"):
			continue;
		if len(var_name) <= 0:
			continue;
		if len(str(type_row[i])) <= 0:
			continue;

		if (type_row[i] == "int"):
			get_list_tmp += blo_getlist_context.replace ("$Type$", type_row[i]).replace ("$classname$", sheet_name).replace ("$paramname$", var_name)
		elif (type_row[i] == "float"):
			get_list_tmp += blo_getlist_context.replace ("$Type$", type_row[i]).replace ("$classname$", sheet_name).replace ("$paramname$", var_name)
		elif (type_row[i] == "double"):
			get_list_tmp += blo_getlist_context.replace ("$Type$", type_row[i]).replace ("$classname$", sheet_name).replace ("$paramname$", var_name)
		elif (type_row[i] == "string"):
			get_list_tmp += blo_getlist_context.replace ("$Type$", type_row[i]).replace ("$classname$", sheet_name).replace ("$paramname$", var_name)
		elif (type_row[i] == "array"):
			continue;
		else:
			continue;


	classnames = sheet_name + "Parse"
	valueNames = classnames[:1].lower() + classnames[1:]

	final_result = filecontext % (charp_blo_namespace, valueNames, get_list_tmp)

	#if (os.path.exists("Blo") == False):
	#	os.mkdir("Blo")
	filePath = dir + "/" + class_api_name + ".cs"    
	apiFiles.append(filePath)
	fp = codecs.open(filePath, "w", "utf_8")
	fp.write(final_result)
	fp.close()

	#fp = codecs.open(cs_file_copy_abs_path + "/Blo/" + class_blo_name + ".cs", "w", "utf_8")
	#fp.write(final_result)
	#fp.close()


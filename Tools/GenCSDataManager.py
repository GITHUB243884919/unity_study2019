import xdrlib, sys
import os
import xlrd
import codecs
import shutil

manager_file_context = """using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace %s
{
    public class LuaConfigManager
    {
    	public static int DictFileNum;

        private static LuaConfigManager instance = new LuaConfigManager ();

        public static LuaConfigManager Instance {
            get {
                return instance;
            }
        }

        private LuaConfigManager ()
        {
        }

        public int fileCount;
        public int initedFileCount { get; private set; }

        public void Init ()
        {
%s
        }

		public IEnumerator InitAsyc ()
        {
        	DateTime a = DateTime.Now;
%s
        }

%s
    }
}
"""

manager_init_file_count_context = """
			fileCount = %s;
			initedFileCount = 0;
"""

manager_init_asyc_file_count_context = """
			fileCount = %s;
"""

manager_dao_fields_context = """
		public %s %s = new %s();
"""

manager_dao_field_init_context = """
			%s.LoadData ();
			initedFileCount ++;
"""

manager_dao_field_init_asyc_context = """
			%s.LoadData ();
			initedFileCount ++;
			if ((DateTime.Now - a).TotalMilliseconds > 20) {
				yield return new WaitForEndOfFrame ();
				a = DateTime.Now;
			}
"""

def Gen(dir, file_name_list, charp_model_namespace, charp_dao_namespace):

	dao_context=""

	file_count = len (file_name_list)
	print(file_count)
	classnames=[]
	valueNames=[]
	for i in range (0, file_count):
		#classnames.append ("Dict" + file_name_list[i] + "Dao")        
		splits = file_name_list[i].split('/')
		fullName = splits[len(splits) - 1]
		className = fullName.replace(".cs", "")
		print(className)
		classnames.append(className)
	for cn in classnames:
		valueNames.append (cn[:1].lower() + cn[1:])

	daos_init_context = manager_init_file_count_context % (str(file_count))
	daos_init_asyc_context = manager_init_asyc_file_count_context % (str(file_count))

	for i in range(0, file_count):
		dao_context += manager_dao_fields_context % (classnames[i], valueNames[i], classnames[i])
		daos_init_context += manager_dao_field_init_context % (valueNames[i])
		daos_init_asyc_context += manager_dao_field_init_asyc_context % (valueNames[i])

	final_result = manager_file_context % (charp_model_namespace, daos_init_context, daos_init_asyc_context, dao_context)

	fp = codecs.open("luaconfig/LuaConfigManager.cs", "w", "utf_8")
	fp.write(final_result)
	fp.close()

	#fp = codecs.open(cs_file_copy_abs_path + "/DictDataManager.cs", "w", "utf_8")
	#fp.write(final_result)
	#fp.close()


import xdrlib, sys
import os
import xlrd
import codecs
import shutil

#import _PlistKit

dao_file_context = """//auto gen code by fanzhengyong

using System;
using System.Collections.Generic;
using UFrame.Json;

namespace %s
{
    public partial class $classname$Parse : JsonConfigBase <$classname$>
    {
        protected override void AddPrimaryIndex($classname$ v)
        {
            cachePrimary[v.ID] = v;
        }

        protected override string GetConfigFileName()
        {
            return "TableData/" + "$classname$" + ".txt";
        }
    }

}
"""

def Gen(dir, sheetName, name_row, type_row, col_num, charp_model_namespace, charp_dao_namespace, parseFiles):
    className = sheetName + "Parse"
    file_context = dao_file_context % (charp_dao_namespace)
    file_context = file_context.replace("$classname$", sheetName);
    filePath = dir + "/" + className + ".cs"
    fp = codecs.open(filePath, "w", "utf_8")
    fp.write(file_context)
    fp.close()
    parseFiles.append(filePath)
    


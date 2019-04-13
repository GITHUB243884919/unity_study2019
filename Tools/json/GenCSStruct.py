import xdrlib, sys
import os
import xlrd
import codecs
import shutil

fileContext = """//auto gen code by fanzhengyong

using UFrame.Json;
namespace %s
{
    public class %s
    {
        #region field
%s
        #endregion
        
        public override string ToString ()
        {
            return %s;
        }
    }
}
"""

fieldContext = """
        /// <summary>
        /// %s
        /// </summary>
        public %s %s;
"""

tostringContext ="""
            +"    %s:"+%s
"""

def Gen(dir, sheetName, name_row, desc_row, type_row, col_num, nameSpace, structFiles):
    fieldText = ""
    className = sheetName
    toStringText = """
            " """ + className + """:"
    """
    
    for i in range(0, col_num):
        var_name = str(name_row[i])
        var_type = str(type_row[i])
        var_desc = str(desc_row[i])
        
        if var_name.startswith("*"):
            continue;
        if len(var_name) <= 0:
            continue;
        if len(var_type) <= 0:
            continue;

        toStringText += tostringContext % (var_name, var_name)

        if (var_type == "int"):
            fieldText += fieldContext % (desc_row[i], var_type, var_name)
        elif (var_type == "float"):
            fieldText += fieldContext % (desc_row[i], var_type, var_name)
        elif (var_type == "double"):
            fieldText += fieldContext % (desc_row[i], var_type, var_name)
        elif (var_type == "string"):
            fieldText += fieldContext % (desc_row[i], var_type, var_name)
        elif (var_type == "array"):
            fieldText += fieldContext % (desc_row[i], "ArrayData", var_name)
        elif (var_type == "INT"):
            fieldText += fieldContext % (var_desc.replace("\n", "\n        /// "), "int", var_name)
        elif (var_type == "STRING"):
            fieldText += fieldContext % (var_desc.replace("\n", "\n        /// "), "string", var_name)
        elif (var_type == "FLOAT"):
            #print(sheetName + " " + desc_row[i])
            fieldText += fieldContext % (var_desc.replace("\n", "\n        /// "), "float", var_name)

    writeContext = fileContext % (nameSpace, className, fieldText, toStringText)
    
    filePath = dir + "/" + className + ".cs"
    fp = codecs.open(filePath, "w", "utf_8")
    fp.write(writeContext)
    fp.close()
    
    #global structFiles
    structFiles.append(filePath)
    

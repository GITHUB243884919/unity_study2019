#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import sys
import codecs
import json
import xlrd
import shutil
import operator

#reload(sys)
#sys.setdefaultencoding('utf-8')

##param##
xlsDir = os.getcwd()  # xls所在目录
outDir = "lua/config"  # 输出目录
copyDir = "../Assets/GameResources/lua/config"
splitStr = "&&"  # 数组分隔符
bErrorFlag = False  # 解析有错

writeFilePaths = []
writeFileNames = []

##函数：table 转化为 txt
def table2Txt(table, fileName):
    global bErrorFlag

    #print "************************************"
    print ("parse: " + table.name + "...")
    nrows = table.nrows
    ncols = table.ncols

    if (nrows < 3):
        bErrorFlag = True
        raise ValueError
        #print ("!!!错误: 表头行不完整,表头行至少3行。"
        print("error must >= 3 lines")
        return

    ## 寻找解析的最后一列
    lastColIndex = ncols - 1  # 最后一列的index

    ## 寻找解析的最后一行
    lastRowIndex = nrows - 1
    print("row=" + str(lastRowIndex) + " col=" + str(lastColIndex)) 
    Table2Lua(table, fileName, lastRowIndex, lastColIndex)


def isKeyVaild(attributeRow, c):
    if (len(attributeRow[c]) > 0 and attributeRow[c].startswith('*') == False):
        return True
    else:
        return False

def isTypeVaild(typeRow, c):
    if (len(typeRow[c]) > 0):
        return True
    else:
        return False


def writeLineData(fileOpen, data):
    strLine = ""
    for m in range(len(data)):
        strLine = strLine + str(data[m])
    writeFile(fileOpen, strLine)

#文件操作begin
def openFileByAdd(filePath):
    fileOpen = codecs.open(filePath, "w", "utf-8")
    return fileOpen


def writeFile(fileOpen, lineStr):
    fileOpen.write(lineStr)
    # fileOpen.write('\n')
    return fileOpen


def closeFile(fileOpen):
    fileOpen.close()    
#文件操作end

def FormatFileName(name):
    return name.lower()
    
    
## 对应平台表转化成txt
def Table2Lua(table, fileName, lastRowIndex, lastColIndex):
    global writeFilePaths
    global writeFileNames
    ## 转化为txt格式
    filePath = outDir  + "/" + FormatFileName(fileName) + ".lua"
    fileOpen = openFileByAdd(filePath)
    
    writeFilePaths.append(filePath)
    writeFileNames.append(FormatFileName(fileName) + ".lua")

    ## 列key数组 & 类型数组
    attributeRow = []
    typeRow = []
    for c in range(lastColIndex + 1):
        ## 列key数组
        strKeyCell = str(table.cell_value(0, c))
        attributeRow.append(strKeyCell)
        ## 类型数组
        strTypeCell = str(table.cell_value(2, c))
        typeRow.append(strTypeCell)
    
    print("type:")
    for var in typeRow:
        print(var)
        
    print("field:")    
    for var in attributeRow:
        print(var)
        
    ## 遍历数据行
    data = []
    data.append("return {\n")
    writeLineData(fileOpen, data)
    for r in range(3, lastRowIndex + 1):
        ##
        if (isKeyVaild(attributeRow, 0) == True and isTypeVaild(typeRow, 0) == True):  ##有key值才解析
            data = []
            cellValue = str(GetData(table.cell_value(r, 0), typeRow[0]))
            data.append("  ["+cellValue + "] = {\n")
            for c in range(lastColIndex + 1):
                if (isKeyVaild(attributeRow, c) == True and isTypeVaild(typeRow, c) == True):  ##有key值才解析
                    cellValue = "    " + attributeRow[c] + " = " + str(GetData(table.cell_value(r, c), typeRow[c]))+','
                    data.append(cellValue+"\n")
            data.append("  },\n")
            writeLineData(fileOpen, data)
    data = []
    data.append("\n}")
    writeLineData(fileOpen, data)
    ## 关闭文件
    closeFile(fileOpen)
    return

def GetData(cellValue, strType):
    global bErrorFlag

    strValue = str(cellValue)

    ## int
    if (operator.eq(strType, "int") == True):
        strValue = "".join(strValue.split())
        if (len(strValue) == 0):
            return 0
        else:
            if (strValue.find('.') != -1):
                strValue = strValue[0:strValue.find('.')]
            try:
                ret = int(strValue)
                return ret
            except ValueError:
                print("error type : " + strType + "=" + strValue)
                bErrorFlag = True
                raise ValueError
                return 0

    ## float
    if (operator.eq(strType, "float") == True):
        strValue = "".join(strValue.split())
        if (len(strValue) == 0):
            return 0
        else:
            try:
                ret = float(strValue)
                return ret
            except ValueError:
                print("error type : " + strType + "=" + strValue)
                bErrorFlag = True
                raise ValueError
                return 0.0

    ## double
    if (operator.eq(strType, "double") == True):
        strValue = "".join(strValue.split())
        if (len(strValue) == 0):
            return 0
        else:
            try:
                ret = float(strValue)
                return ret
            except ValueError:
                print("error type : " + strType + "=" + strValue)
                bErrorFlag = True
                raise ValueError
                return 0.0

    ## string
    if (operator.eq(strType, "string") == True):
        if (len(strValue) == 0):
            return '\"'+""+'\"'
        else:
            if(strValue.endswith('.0')):
                strValue = strValue[0:strValue.find('.')]
            return '\"'+strValue+'\"'

    if (operator.eq(strType, "array") == True):
        if (len(strValue) == 0):
            return "{}"
        else:
            strValue = strValue.replace('[','{')
            strValue = strValue.replace(']', '}')
            return strValue

    ## array_int
    if (operator.eq(strType, "array_int") == True):
        strValue = "".join(strValue.split())
        arrayStr = strValue.split(splitStr)
        for m in range(len(arrayStr)):
            if (len(arrayStr[m]) > 0):
                retStr = arrayStr[m]
                if (retStr.find('.') != -1):
                    retStr = retStr[0:retStr.find('.')]
                try:
                    k = int(retStr)
                except ValueError:
                    print("error type : " + strType + "=" + strValue)
                    bErrorFlag = True
                    raise ValueError
        return strValue

    ## array_float
    if (operator.eq(strType, "array_float") == True):
        strValue = "".join(strValue.split())
        arrayStr = strValue.split(splitStr)
        for m in range(len(arrayStr)):
            if (len(arrayStr[m]) > 0):
                try:
                    k = float(arrayStr[m])
                except ValueError:
                    print("error type : " + strType + "=" + strValue)
                    bErrorFlag = True
                    raise ValueError
        return strValue

    ## array_string
    if (operator.eq(strType, "array_string") == True):
        strValue = "".join(strValue.split())
        return strValue

    ## array_string_free
    if (operator.eq(strType, "array_string_free") == True):
        return strValue

    if (operator.eq(strType, "boolean") == True):
        if(strValue == '0'):
            return "False"
        else:
            return "True"
    ## 未处理的类型，报错
    #print u"错误：未识别的类型" + strType
    print ("error: unkown type" + strType)
    bErrorFlag = True
    raise ValueError

    return

#最后所有的lua配置文件需要加载
def WriteLuaInit(filePath):
    global writeFileNames
    fileOpen = openFileByAdd(filePath+'/init_lua_config.lua')
    ret = 'config = {};local m = config;local g = _G;local rawget = rawget;local rawset = rawset;_ENV = setmetatable({},{__index = function(t,k) return rawget(m,k) or rawget(g,k) end,__newindex = m});\n'
    writeFile(fileOpen,ret)
    index = 0
    for writeFilePath in writeFilePaths:
        print (writeFilePath)

        shutil.copy(writeFilePath,copyDir+'/'+writeFileNames[index])
        fname = writeFileNames[index].split(".")[0]

        ret =  "config_"+fname + ' = require "' + fname + '"' + "\n"
        writeFile(fileOpen,ret)

        index = index + 1
    closeFile(fileOpen)
    shutil.copy(filePath+'/init_lua_config.lua',copyDir+'/'+'init_lua_config.lua')
    
    
def main(argv):
    print ("=======================================================")

    ## 处理输入参数
    global xlsDir
    global outDir
    if len(argv) >= 2:
        xlsDir = argv[1]
        print ("prama1:" + argv[1])

    if len(argv) >= 3:
        outDir = argv[2]
        print ("prama2:" + argv[2])

    ## 创建输出文件夹
    if (os.path.exists(outDir) == True):
        shutil.rmtree(outDir, True)
    print("xlsDir " + xlsDir)
    print("outDir " + outDir)
    os.mkdir(outDir)
    

    for fn in os.listdir(xlsDir):
        if ((fn.find(".xlsx") >= 0) and fn.find("~") < 0):
            print(fn)
            print(xlsDir + os.path.sep + fn + "...")
            data = xlrd.open_workbook(xlsDir + os.path.sep + fn)
            sheets = data.sheets()
            for sheet in sheets:
                if (sheet.name.find("_") == 0 or sheet.name.find("#") == 0):
                    continue
                print(sheet.name)
                table2Txt(sheet, sheet.name)
	
    WriteLuaInit(xlsDir + "/" + outDir)
    
if __name__ == '__main__':
    main(sys.argv)
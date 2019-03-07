#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import sys
import codecs
import json
import xlrd
import shutil
import operator
import GenCSStruct
import GenCSParse
import GenCSAPI
import GenCSDataManager

xlsDir = os.getcwd()
outDir = "luaconfig"
#����Ŀ¼
#CSCode/Struct ���ݽṹ����
#CSCode/API    ���ʽӿ�
#CSCode/Parse  �����ӿ�

structDir = outDir + "/struct"
apiDir = outDir + "/api"
parseDir = outDir + "/parse"
namespace = "Game.ToLua.Config"

structFiles = []
apiFiles = []
parseFiles = []

copyDir = "../Assets/Scripts/"

def MakeDir():    
    if (os.path.exists(outDir) == True):
        shutil.rmtree(outDir, True)
    os.mkdir(outDir) 
    
    if (os.path.exists(structDir) == True):
        shutil.rmtree(structDir, True)
    os.mkdir(structDir) 
    
    if (os.path.exists(apiDir) == True):
        shutil.rmtree(apiDir, True)
    os.mkdir(apiDir) 
    
    if (os.path.exists(parseDir) == True):
        shutil.rmtree(parseDir, True)
    os.mkdir(parseDir) 
    
    
#�����������lua�ļ���
def GenInitCode():
    return

def GenCode(fileName):
    data = xlrd.open_workbook(xlsDir + os.path.sep + fileName)
    sheets = data.sheets()
    for sheet in sheets:
        if (sheet.name.find("_") == 0 or sheet.name.find("#") == 0):
            continue
            
        nrows = sheet.nrows
        ncols = sheet.ncols
        nameRow = sheet.row_values(0)
        descRow = sheet.row_values(1)
        typeRow = sheet.row_values(2)
        
        print("gen " + sheet.name + "'s struct file " + sheet.name + ".cs")
        GenCSStruct.Gen(structDir, sheet.name, nameRow, descRow, typeRow, ncols, namespace, structFiles)
        
        print("gen " + sheet.name + "'s parse file " + sheet.name + "parse" + ".cs")
        GenCSParse.Gen(parseDir, sheet.name, nameRow, typeRow, ncols, namespace, namespace, parseFiles)
        
        print("gen " + sheet.name + "'s api file " + sheet.name + "API" + ".cs")
        print(apiDir)
        GenCSAPI.Gen(apiDir, sheet.name, nameRow, typeRow, ncols, namespace, namespace, apiFiles)

    
def main(argv):
    MakeDir()
    for fileName in os.listdir(xlsDir):
        if ((fileName.find(".xlsx") >= 0) and fileName.find("~") < 0):
            print("begin parse " + fileName)
            GenCode(fileName)
            print("end parse " + fileName)
    for var in structFiles:
        print("struct files " + var);        
        shutil.copy(var, copyDir + '/'+ var)
    
    for var in parseFiles:
        print("parse files " + var);        
        shutil.copy(var, copyDir + '/'+ var)
    
    for var in apiFiles:
        print("api files " + var);        
        shutil.copy(var, copyDir + '/'+ var)    
        
    print("gen manager")
    GenCSDataManager.Gen("luaconfig/", parseFiles, namespace, namespace)
    shutil.copy("luaconfig/LuaConfigManager.cs", copyDir + outDir + "/" + "LuaConfigManager.cs")
    
    GenInitCode()
   
if __name__ == '__main__':
    main(sys.argv)
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
#import GenCSAPI
import GenCSDataManager

xlsDir = os.getcwd()
#outDir = "luaconfig"
outDir = "auto_gen_json_parse"

#三个目录
#CSCode/Struct 数据结构定义
#CSCode/API    访问接口
#CSCode/Parse  解析接口

#structDir = outDir + "/struct"
#apiDir = outDir + "/api"
#parseDir = outDir + "/parse"

structDir = outDir
apiDir = outDir
parseDir = outDir

namespace = "Game.Json.Config"

structFiles = []
apiFiles = []
parseFiles = []
blackList = []

copyDir = "../Assets/Scripts"

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
    

def GenCode(fileName):
    data = xlrd.open_workbook(xlsDir + os.path.sep + fileName)
    sheets = data.sheets()
    for sheet in sheets:
        #if (sheet.name.find("_") == 0 or sheet.name.find("#") == 0):
        #    continue
        if (sheet.name != fileName.replace(".xlsx", "")):
            continue
        
        nrows = sheet.nrows
        ncols = sheet.ncols
        nameRow = sheet.row_values(0)
        descRow = sheet.row_values(1)
        typeRow = sheet.row_values(2)
        
        print("    gen " + sheet.name + "'s struct file " + sheet.name + ".cs")
        GenCSStruct.Gen(structDir, sheet.name, nameRow, descRow, typeRow, ncols, namespace, structFiles)
        
        print("    gen " + sheet.name + "'s parse file " + sheet.name + "Parse.cs")
        GenCSParse.Gen(parseDir, sheet.name, nameRow, typeRow, ncols, namespace, namespace, parseFiles)
        
        #print("gen " + sheet.name + "'s api file " + sheet.name + "API" + ".cs")
        #print(apiDir)
        #GenCSAPI.Gen(apiDir, sheet.name, nameRow, typeRow, ncols, namespace, namespace, apiFiles)

def ReadBlackList():
    fp = codecs.open("BlackList.txt", "r")
    
    while True:
        line = fp.readline()
        if not line:
            break
        line = line.replace("\n", "")
        print("[" + line + "]")
        blackList.append(line)
        
    fp.close()
    
    
def main(argv):
    MakeDir()
    ReadBlackList()
    for fileName in os.listdir(xlsDir):
        #if ((fileName.find(".xlsx") >= 0) and fileName.find("~") < 0 and fileName == "ActivityAccount.xlsx"):
        if ((fileName.find(".xlsx") >= 0) and fileName.find("~") < 0):
            for blackListFileName in blackList:
                if (blackListFileName != fileName):
                    print("Begin Parse " + fileName)
                    GenCode(fileName)
            print("End parse " + fileName)

    
    #for var in apiFiles:
    #    print("api files " + var);        
    #    shutil.copy(var, copyDir + '/'+ var)    
        
    print("\ngen manager JsonConfigManger.cs\n")
    GenCSDataManager.Gen("", parseFiles, namespace, namespace)

    for var in structFiles:
        print("copy struct files " + var);        
        shutil.copy(var, copyDir + '/'+ var)
    
    for var in parseFiles:
        print("copy parse files " + var);        
        shutil.copy(var, copyDir + '/'+ var)
        
    print("copy manager JsonConfigManger.cs")
    shutil.copy("auto_gen_json_parse/JsonConfigManger.cs", copyDir + "/" + outDir + "/" + "JsonConfigManger.cs")
   
if __name__ == '__main__':
    main(sys.argv)
---
---  通用接口工具类
---

local LuaCommon_Utils = Class("LuaCommon_Utils")

LuaCommon_Utils._instance  = nil
-- 可读写的路径
LuaCommon_Utils.AssetPath = ""
-- lua脚本的根目录
LuaCommon_Utils.LuaDir = UnityEngine.Application.dataPath .. "/Lua"

-- override 初始化各种数据
function LuaCommon_Utils.initialize()

end

function LuaCommon_Utils.InstantiateGoById(id,parent)
    local resConfig = ConfigMgr.Instance():GetItem("ResPathConfig",id)
    if resConfig and resConfig.path then
        return Common_Utils.InstantiateGoByPath(resConfig.path,parent)
    else
        error("ResPathConfig表中未配置"..id)
        return nil
    end
end

function LuaCommon_Utils.GetResourceByPath(path,type,resLoadMode)
    return LuaResourceMgr.GetInstance():GetResourceByPath(path,type,resLoadMode)
end

function LuaCommon_Utils.GetResourceByPathAsync(path,type,resLoadMode,callback)
    LuaResourceMgr.GetInstance():GetResourceByPathAsync(path,type,resLoadMode,callback)
end

function LuaCommon_Utils.GetResourceById(id,type,resLoadMode)
    local resConfig = ConfigMgr.Instance():GetItem("ResPathConfig",id)
    if resConfig and resConfig.path then
        return LuaResourceMgr.GetInstance():GetResourceByPath(resConfig.path,type,resLoadMode)
    else
        error("ResPathConfig表中未配置"..id)
        return nil
    end
end

function LuaCommon_Utils.GetResourceByIdAsync(id,type,resLoadMode,callback)
    local resConfig = ConfigMgr.Instance():GetItem("ResPathConfig",id)
    if resConfig and resConfig.path then
        LuaResourceMgr.GetInstance():GetResourceByPathAsync(resConfig.path,type,resLoadMode,callback)
    else
        error("ResPathConfig表中未配置"..id)
    end
end

return LuaCommon_Utils
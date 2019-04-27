---
--- Module类的管理器
---

local ModuleManager = Class("ModuleManager")
local Modules = require("Game.Main.Modules")

ModuleManager._instance = nil

function ModuleManager.Instance()
    if nil == ModuleManager._instance then
        ModuleManager._instance = ModuleManager:new()
    end
    return ModuleManager
end

-- override 初始化各种数据
function ModuleManager:initialize()
    self.moduleList = {}
end

function ModuleManager:RegisterModule(module)
    local moduleId = module:GetModuleId()
    self.moduleList[moduleId] = module
end

function ModuleManager:GetModule(moduleId)
    return self.moduleList[moduleId]
end

function ModuleManager:InitModule(moduleId)
    local module = self.moduleList[moduleId]
    if(module and false == module:IsInit()) then
        module:Init()
    end
end

function ModuleManager:InitAllModules()
    for i = 1,#Modules.moduleList do
        local module = Modules.moduleList[i].Instance()
        if(module and false == module:IsInit()) then
            module:Init()
        end
    end
end

function ModuleManager:RegisterAllModules()
    for i =1,#Modules.moduleList do
        local module = Modules.moduleList[i].Instance()
        self:RegisterModule(module)
    end
end

function ModuleManager:ResetAllModules()
    self:ResetAllModulesWithExcept(nil)
end

function ModuleManager:ResetAllModulesWithExcept(exceptList)
    for i =1,#Modules.moduleList do
        local module = Modules.moduleList[i].Instance()
        local moduleId = module:GetModuleId()
        if nil == exceptList or not exceptList[moduleId] then
            module:Reset()
        end
    end
end

function ModuleManager:ExitAllModules()
    self:ExitAllModulesWithExcept(nil)
end

function ModuleManager:ExitAllModulesWithExcept(exceptList)
    for i =1,#Modules.moduleList do
        local module = Modules.moduleList[i].Instance()
        local moduleId = module:GetModuleId()
        if nil == exceptList or not exceptList[moduleId] then
            module:Exit()
        end
    end
end

return ModuleManager
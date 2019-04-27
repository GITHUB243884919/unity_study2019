---
--- ModuleBase基类
---

local ModuleBase = Class("ModuleBase")

-- override 初始化各种数据
function ModuleBase:initialize()
    self.moduleId = 0
    self.isInit = false
end

function ModuleBase:Init()
    if not self.isInit then
        self.isInit = true
        self:RegisterNetListener()
        self:RegisterEventListener()
    end
end

function ModuleBase:Exit()
    self.isInit = false
    self:UnRegisterNetListener()
    self:UnRegisterEventListener()
end

function ModuleBase:Reset()
    self:Exit()
    self:Init()
    self:OnReset()
end

function ModuleBase:OnReset()

end

function ModuleBase:RegisterNetListener()

end

function ModuleBase:RegisterEventListener()

end

function ModuleBase:UnRegisterNetListener()

end

function ModuleBase:UnRegisterEventListener()

end

function ModuleBase:IsInit()
    return self.isInit
end

function ModuleBase:GetModuleId()
    return self.moduleId
end

function ModuleBase:SetModuleId(moduleId)
    self.moduleId = moduleId
end

return ModuleBase
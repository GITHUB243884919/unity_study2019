---
--- UISettingPanel UI类
---

local UIBase = require("Core.ui.UIBase")
local UISettingPanel = Class("UISettingPanel",UIBase)

local _instance = nil

function UISettingPanel.Instance()
    if nil == _instance then
        _instance = UISettingPanel:new()
    end
    return _instance
end

-- virtual 子类可以初始化一些变量,ResId要在这里赋值
function UISettingPanel:InitParam()
    self.ResId = 103
    self:ShowUIBlur(false)
end

-- override UI面板创建结束后调用，可以在这里获取gameObject和component等操作
function UISettingPanel:OnCreate()

end

-- 界面可见性变化的时候触发
function UISettingPanel:OnShow(isShow)

end

-- 界面销毁的过程中触发
function UISettingPanel:OnDestroy()

end

-- 注册UI事件监听
function UISettingPanel:RegisterEvent()

end

-- 取消注册UI事件监听
function UISettingPanel:UnRegisterEvent()

end

------------------- UI事件回调 --------------------------
function UISettingPanel:onClick(obj)

end

function UISettingPanel:onBoolValueChange(obj, isSelect)

end

---------------------- UI事件回调 --------------------------

return UISettingPanel
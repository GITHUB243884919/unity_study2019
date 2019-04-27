---
--- UIDebugPanel UI类
---

local UIBase = require("Core.ui.UIBase")
local UIDebugPanel = Class("UIDebugPanel", UIBase)

local _instance = nil

-- virtual 子类可以初始化一些变量,ResId要在这里赋值
function UIDebugPanel:InitParam()
    self.ResId = 102
    self.uiDepthLayer = ECEnumType.UIDepth.DEBUG
end

function UIDebugPanel.Instance()
    if nil == _instance then
        _instance = UIDebugPanel:new()
    end
    return _instance
end

-- override UI面板创建结束后调用，可以在这里获取gameObject和component等操作
function UIDebugPanel:OnCreate()
    self.DebugText = self.Panel:FindChildByPath("ScrollView/Viewport/Text"):GetComponent("UnityEngine.UI.Text")
    if self.DebugText then
        Common_Utils.AttachScreenText(self.DebugText)
    end
end

-- 界面可见性变化的时候触发
function UIDebugPanel:OnShow(isShow)

end

-- 界面销毁的过程中触发
function UIDebugPanel:OnDestroy()
    UIBase.OnDestroy(self)
    self.DebugText = nil
    print("-------->PanelName",self.PanelName)
    print("-------->uiCanvas",self.uiCanvas)
end

-- 注册UI事件监听
function UIDebugPanel:RegisterEvent()

end

-- 取消注册UI事件监听
function UIDebugPanel:UnRegisterEvent()

end

------------------- UI事件回调 --------------------------
function UIDebugPanel:onClick(obj)
    if obj.name == "BtnClose" then
        Common_Utils.UnAttachScreenText()
        UIManager.Instance():Close(ECEnumType.UIEnum.DebugPanel)
    elseif obj.name == "BtnClear" then
        Common_Utils.ClearSreenLog()
    end
end

function UIDebugPanel:onBoolValueChange(obj, isSelect)

end

---------------------- UI事件回调 --------------------------

return UIDebugPanel
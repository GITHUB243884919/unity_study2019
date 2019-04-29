local UIBase = require("UFrame.Game.UI.UIBase")
local UI_Login = Class("UI_Login",UIBase)

local _instance = nil

function UI_Login.Instance()
    if nil == _instance then
        _instance = UI_Login:new()
    end
    return _instance
end

-- virtual 子类可以初始化一些变量,ResId要在这里赋值
function UI_Login:InitParam()
    --self.ResId = 100
    self.ResPath = "prefabs/ui/ui_login"
    self.uiDepthLayer = UIDepth.NORMAL
    self.sortEnable = false
    --self:ShowUIBlur(true)
end

-- override UI面板创建结束后调用，可以在这里获取gameObject和component等操作
function UI_Login:OnCreate()

end

-- 界面可见性变化的时候触发
function UI_Login:OnShow(isShow)
end

function UI_Login:onClick(obj)
    if obj.name == "Button" then
        --UIManager.Instance():Open(ECEnumType.UIEnum.DebugPanel)
        print("Button Click")
    end
end

return UI_Login
---
--- 通用的Loading界面
---
local UIBase = require("Core.ui.UIBase")
local UILoading = Class("UILoading",UIBase)

local _instance = nil

-- virtual 子类可以初始化一些变量,ResId要在这里赋值
function UILoading:InitParam()
    self.ResId = 101
    self.uiDepthLayer = ECEnumType.UIDepth.BOTTOMMOST
end

function UILoading.Instance()
    if nil == _instance then
        _instance = UILoading:new()
    end
    return _instance
end

return UILoading
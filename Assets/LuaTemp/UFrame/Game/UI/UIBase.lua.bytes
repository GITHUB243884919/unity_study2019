---
--- UIBase基类
---

local UIBase = Class("UIBase")

-- override 初始化各种数据
function UIBase:initialize()
    self.Panel = nil
    self.ResId = 0
    self.ResPath = ""
    self.Layer = 0
    self.UILevel = 0
    self.subUIList = {}
    self.uiDepthLayer = UIDepth.NORMAL
    self.uiCanvas = nil
    self.sortEnable = true
    self.sorterTag = nil
    self.uguiMsgHandler = nil
    self.PanelName = ""
    self.isExist = false
    self.isShowUIBlur = false
    self:InitParam()
end

-- virtual 子类可以初始化一些变量,ResId要在这里赋值
function UIBase:InitParam()

end

-- 对外调用，用于创建UI
function UIBase:Create()
    if nil ~= self.Panel then
        UnityEngine.GameObject.Destroy(self.Panel)
    end
    --self.Panel = UTL.LuaCommon.InstantiateGoById(self.ResId, Common_Utils.GetUIRootObj())
    print(self.ResPath)
    local getter  = ResHelper.LoadGameObject(self.ResPath)
    self.Panel = getter:Get()
    print("UIBASE Create " , UIManager.Instance():GetUIRoot())
    self.Panel.transform:SetParent(UIManager.Instance():GetUIRoot(), false)
    self.PanelName = self.Panel.name
    self.Layer = self.Panel.layer
    ---- 如果参与UI排序
    --if self.sortEnable then
    --    self.sorterTag = self.Panel:AddSingleComponent(typeof(SorterTag))
    --    self.uiCanvas = self.Panel:AddSingleComponent(typeof(UnityEngine.Canvas))
    --    self.Panel:AddSingleComponent(typeof(UnityEngine.UI.GraphicRaycaster))
    --    self.uiCanvas.overrideSorting = true
    --    self.Panel:AddSingleComponent(typeof(ParticleOrderAutoSorter))
    --    UIManager.Instance():GetUISorterMgr():AddPanel(self)
    --end
    --self.isExist = true
    --if self.isShowUIBlur then
    --    UIManager.Instance():ShowUIBlur(self)
    --end
    self:AttachListener(self.Panel)
    self:OnCreate()
    self:RegisterEvent()
    self:OnShow(self:IsVisible())
end

----对外调用，用于创建UI，不走ResId加载，直接由现有gameObject创建
--function UIBase:CreateWithGo(gameObejct)
--    self.Panel = gameObejct
--    self.PanelName = self.Panel.name
--    self.Layer = self.Panel.layer
--    if self.sortEnable then
--        self.sorterTag = self.Panel:AddSingleComponent(typeof(SorterTag))
--        self.uiCanvas = self.Panel:AddSingleComponent(typeof(UnityEngine.Canvas))
--        self.Panel:AddSingleComponent(typeof(UnityEngine.GraphicRaycaster))
--        self.uiCanvas.overrideSorting = true
--        self.Panel:AddSingleComponent(typeof(ParticleOrderAutoSorter))
--        UIManager.Instance():GetUISorterMgr():AddPanel(self)
--    end
--    self.isExist = true
--    if self.isShowUIBlur then
--        UIManager.Instance():ShowUIBlur(self)
--    end
--    self:AttachListener(self.Panel)
--    self:OnCreate()
--    self:RegisterEvent()
--    self:OnShow(self:IsVisible())
--end

-- override UI面板创建结束后调用，可以在这里获取gameObject和component等操作
function UIBase:OnCreate()

end

-- 界面可见性变化的时候触发
function UIBase:OnShow(isShow)

end

-- 设置界面可见性
function UIBase:SetVisible(isVisible)
    self.Panel:SetActive(isVisible)
    self:OnShow(isVisible)
end

-- 返回界面的可见性
function UIBase:IsVisible()
    return nil ~= self.Panel and self.Panel.activeSelf
end

-- 返回界面实例是否存在
function UIBase:IsExist()
    return self.isExist
end

-- 销毁一个UI界面
function UIBase:Destroy()
    if self.sortEnable then
        UIManager.Instance():GetUISorterMgr():RemovePanel(self)
    end
    self:DestroySubPanels()
    self:UnRegisterEvent()
    self:UnAttachListener(self.Panel)
    --if self.UILevel == UILevel.Level1 then
    --    GUIHelper.GetModelOutlineCameraObj().GetComponent("ImageEffectUIBlur").FinalTexture = nil
    --end
    if nil ~= self.Panel then
        --if 0 ~= self.ResId then
        --    UnityEngine.GameObject.Destroy(self.Panel)
        --    self.Panel = nil
        --else
        --    self:SetVisible(false)
        --end
        UnityEngine.GameObject.Destroy(self.Panel)
    end
    self.isExist = false
    self:OnDestroy()
end

-- 界面销毁的过程中触发
function UIBase:OnDestroy()
    self.Panel = nil
    self.Layer = 0
    self.uiCanvas = nil
    self.sorterTag = nil
    self.uguiMsgHandler = nil
    self.PanelName = ""
    self.isExist = false
    self.isShowUIBlur = false
end

-- 关联子UI，统一参与管理
function UIBase:AttachSubPanel(subPanelPath, subUI, uiLevel)
    if nil == subPanelPath or subPanelPath == "" then
        return
    end
    local subUIObj = self.Panel:FindChildByPath(subPanelPath)
    if nil ~= subUIObj then
        subUI:CreateWithGo(subUIObj, uiLevel)
        table.insert(self.subUIList, subUI)
    end
end

-- 将一个UI界面注册为本UI的子界面，统一参与管理
function UIBase:RegisterSubPanel(subUI)
    if nil == subUI then
        return
    end
    subUI.uiDepthLayer = self.uiDepthLayer
    table.insert(self.subUIList, subUI)
end

-- 解除子UI关联
function UIBase:DetchSubPanel(subUI)
    if nil ~= self.subUIList then
        table.remove(subUI)
    end
end

--  销毁关联的子面板，不要重写
function UIBase:DestroySubPanels()
    if nil ~= self.subUIList then
        for _, v in ipairs(self.subUIList) do
            v:Destroy()
            v.Panel = nil
        end
    end
    for _, v in pairs(self.subUIList) do
        v = nil
    end
    self.subUIList = {}
end

-- 将当前UI层级提高，展示在当前Level的最上层
function UIBase:BringTop()
    UIManager.Instance():GetUISorterMgr():MovePanelToTop(self)
end

-- 将当前UI提升到指定UIDepthLayer的最上层
function UIBase:BringToTopOfLayer(depthLayer)
    UIManager.Instance():GetUISorterMgr():MovePanelToTopOfLayer(self, depthLayer)
end

-- 显示UI背景模糊
function UIBase:ShowUIBlur(isShow)
    self.isShowUIBlur = isShow
end

-- 设置点击外部关闭(执行该方法以后，当点击其他UI的时候，会自动关闭本UI)
function UIBase:SetOutTouchDisappear()
    UIManager.Instance():SetOutTouchDisappear(self)

end

-- 注册UIEventListener
function UIBase:AttachListener(gameObject)
    if nil == gameObject then
        return
    end
    local uguiMsgHandler = gameObject:GetComponent("UGUIMsgHandler")
    if uguiMsgHandler == nil then
        uguiMsgHandler = gameObject:AddComponent(typeof(UGUIMsgHandler))
    end
    self.uguiMsgHandler = uguiMsgHandler

    -- BindFunction
    self.uguiMsgHandler.onClick = function(obj)
        self:onClick(obj)
    end
    self.uguiMsgHandler.onBoolValueChange = function(obj, isSelect)
        self:onBoolValueChange(obj, isSelect)
    end
    self.uguiMsgHandler.onEvent = function(eventName)
        self:onEvent(eventName)
    end
    self.uguiMsgHandler.onFloatValueChange = function(obj, value)
        self:onFloatValueChange(obj, value)
    end
    self.uguiMsgHandler.onStrValueChange = function(obj, text)
        self:onStrValueChange(obj, text)
    end
    self.uguiMsgHandler.onDrag = function(obj, deltaPos, curToucPosition)
        self:onDrag(obj, deltaPos, curToucPosition)
    end
    self.uguiMsgHandler.onBeginDrag = function(obj, deltaPos, curToucPosition)
        self:onBeginDrag(obj, deltaPos, curToucPosition)
    end
    self.uguiMsgHandler.onEndDrag = function(obj, deltaPos, curToucPosition)
        self:onEndDrag(obj, deltaPos, curToucPosition)
    end

    self.uguiMsgHandler:AttachListener(gameObject)
end

function UIBase:UnAttachListener(gameObject)
    if nil == gameObject then
        return
    end
    if self.uguiMsgHandler then
        self.uguiMsgHandler:UnAttachListener(gameObject)
    end
    --UnBindFunction
    self.uguiMsgHandler.onClick = nil
    self.uguiMsgHandler.onBoolValueChange = nil
    self.uguiMsgHandler.onEvent = nil
    self.uguiMsgHandler.onFloatValueChange = nil
    self.uguiMsgHandler.onStrValueChange = nil
    self.uguiMsgHandler.onDrag = nil
    self.uguiMsgHandler.onBeginDrag = nil
    self.uguiMsgHandler.onEndDrag = nil

    self.uguiMsgHandler = nil

end

-- 注册UI事件监听
function UIBase:RegisterEvent()

end

-- 取消注册UI事件监听
function UIBase:UnRegisterEvent()

end

------------------- UI事件回调 --------------------------
function UIBase:onClick(obj)

end

function UIBase:onBoolValueChange(obj, isSelect)

end

function UIBase:onEvent(eventName)
    --if eventName == "onClick" then
    --    UIManager.Instance():NotifyDisappear(self.PanelName)
    --end
end

function UIBase:onFloatValueChange(obj, value)

end

function UIBase:onStrValueChange(obj, text)

end

function UIBase:onDrag(obj, deltaPos, curToucPosition)

end

function UIBase:onBeginDrag(obj, deltaPos, curToucPosition)

end

function UIBase:onEndDrag(obj, deltaPos, curToucPosition)

end
---------------------- UI事件回调 --------------------------

return UIBase
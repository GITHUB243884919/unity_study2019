---
--- UIManager UI管理器
---

--local GUICollections = require("Game.Main.GUICollections")
local UISorter = require("UFrame.Mgrs.UISorter")
local UIManager = Class("UIManager")

UIManager._instance = nil

function UIManager:initialize()
    print("UIManager:initialize")
    self.uiList = {}  -- 存储打开的UI列表
    self.outTouchList = {} -- 用于存储参与点击其他地方关闭面板管理的UI的列表
    self.removeList = {} -- 存储要进行统一关闭面板的列表
    self.recordList = {} -- 存储统一隐藏/恢复显示的UI列表

    self.uiSorter = UISorter.Create(1, 3800)

    self.uiPanels = {}
    self.uiRoot = nil
    -- 注册事件
    self:RegisterEvent()
end

function UIManager.Instance()
    if nil == UIManager._instance then
        UIManager._instance = UIManager:new()
        UIManager._instance.uiRoot = UnityEngine.GameObject.Find("game_root/ui_root").transform
    end
    return UIManager._instance
end

function UIManager:RegisterUI(k, v)
    self.uiPanels[k] = v
end

function UIManager:GetUIRoot()
    return self.uiRoot
end

-- 注册事件
function UIManager:RegisterEvent()
    -- 创建界面
    EventManager.Instance():AddEventListener(MessageCode.UIMsg_CreateUI, function(UIEnum)
        print("create ui " .. UIEnum)
        self:Open(UIEnum)
    end)
    -- 销毁界面
    EventManager.Instance():AddEventListener(MessageCode.UIMsg_DestroyUI, function(UIEnum)
        self:Close(UIEnum)
    end)
end

---- 设置一个UI界面参与点击其他地方关闭面板管理
--function UIManager:SetOutTouchDisappear(ui)
--    -- 把UI加到outlist里面
--    local isContain = false
--    for _, v in ipairs(self.outTouchList) do
--        if v == ui then
--            isContain = true
--        end
--    end
--    if not isContain then
--        table.insert(self.outTouchList, 1, ui)
--    end
--end
--
---- 分发处理点击其他地方关闭面板
--function UIManager:NotifyDisappear(panelName)
--    self.removeList = {}
--    for k, v in ipairs(self.outTouchList) do
--        if nil ~= v and v.PanelName ~= panelName then
--            v:Destroy()
--            self.removeList[k] = true
--            break  --每次只关闭一个界面
--        end
--    end
--    -- 从outTouch列表中移除已经关闭的UI界面
--    for i = #self.outTouchList, 1, -1 do
--        if self.removeList[i] then
--            table.remove(self.outTouchList, i)
--        end
--    end
--end

-- 打开一个UI
function UIManager:Open(UIEnum)
    local uiPanel = self.uiPanels[UIEnum]
    if (uiPanel == nil) then
        return
    end

    if uiPanel.Instance():IsExist() == false then
        uiPanel.Instance():Create()
        table.insert(self.uiList, uiPanel.Instance())
    end
end

-- 关闭一个UI
function UIManager:Close(UIEnum)
    local uiPanel = uiPanels[UIEnum]
    if (uiPanel == nil) then
        return
    end

    uiPanel.Instance():Destroy()

    --移除uiList中的UI实例
    local rmIndex = -1
    for i = #self.uiList, 1, -1 do
        if self.uiList[i] == uiPanel.Instance() then
            rmIndex = i
            break
        end
    end
    if -1 ~= rmIndex then
        table.remove(self.uiList, rmIndex)
    end

end

---- 根据UI枚举获得UI界面实例
--function UIManager:GetViewByType(UIEnum)
--    if GUICollections and GUICollections[UIEnum] then
--        return GUICollections[UIEnum].Instance()
--    end
--    return nil
--end

---- 判断一个UI是否存在于界面上
--function UIManager:IsExist(UIEnum)
--    if GUICollections and GUICollections[UIEnum] then
--        return GUICollections[UIEnum].Instance():IsExist()
--    end
--    return false
--    -- 不用这种遍历的判断方式了
--    --if self.uiList then
--    --    for _, v in ipairs(self.uiList) do
--    --        if GUICollections[UIEnum].Instance() == v then
--    --            return true
--    --        end
--    --    end
--    --end
--    --return false
--end

---- 恢复显示之前记录下来的隐藏UI
--function UIManager:PopAndShowAllUI()
--    if self.recordList and next(self.recordList) ~= nil then
--        for _, v in ipairs(self.recordList) do
--            v:SetVisible(true)
--        end
--    end
--    self.recordList = {}
--    EventMgr.Instance():DispatchEvent(Modules.moduleId.Common, Modules.notifyId.Common.ALLUI_SHOWSTATE_CHANGED, { state = true })
--end
--
---- 记录并隐藏除了指定类型的当前显示的所有UI
--function UIManager:StashAndHideAllUI(UIEnum, extUI)
--    self.recordList = {}
--    for _, v in ipairs(self.uiList) do
--        if v and v:IsVisible() and v.PanelName ~= extUI.PanelName then
--            table.insert(self.recordList, v)
--            v:SetVisible(false)
--        end
--    end
--    EventMgr.Instance():DispatchEvent(Modules.moduleId.Common, Modules.notifyId.Common.ALLUI_SHOWSTATE_CHANGED, { state = false })
--end
--
---- 统一关闭属于某一UI层
--function UIManager:CloseUIByLevel(level)
--    if self.uiList then
--        -- 倒序关闭
--        for i = #self.uiList, 1, -1 do
--            self.uiList[i]:Destroy()
--        end
--    end
--    self.uiList = {}
--end
--
----获取最近一次打开的面板
--function UIManager:GetLastOpenPanel()
--    if self.uiList then
--        return self.uiList[#self.uiList]
--    end
--    return nil
--end
--
---- 显示UI背景模糊
--function UIManager:ShowUIBlur(ui)
--    Common_Utils.ShowUIBlur(ui.Panel,ui.PanelName)
--end

--  获取UI排序管理器
function UIManager:GetUISorterMgr()
    return self.uiSorter
end

return UIManager
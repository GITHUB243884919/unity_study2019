--[[Notice:This lua config file is auto generate by Xls2Lua Tools，don't modify it manually! --]]
local fieldIdx = {}
fieldIdx.id = 1
fieldIdx.path = 2
fieldIdx.resType = 3
fieldIdx.resLiveTime = 4
local data = {
{100,[[Arts/Gui/Prefabs/uiLoginPanel.prefab]],0,20},
{101,[[Arts/Gui/Prefabs/uiloading.prefab]],0,-2},
{102,[[Arts/Gui/Prefabs/UIDebugPanel.prefab]],0,-2},
{103,[[Arts/Gui/Prefabs/UISettingPanel.prefab]],0,-2},
{2001,[[Arts/Gui/Textures/atlas/airfightSheet.prefab]],0,-2},
{300001,[[Arts/Gui/Material/material_defaultgray.mat]],0,-2},
{400001,[[Arts/Gui/Textures/head_icon/GuChenSha.png]],0,-2},
{400002,[[Arts/Gui/Textures/texture/loading/LoadingBackground0.png]],0,-2},}
local mt = {}
mt.__index = function(a,b)
	if fieldIdx[b] then
		return a[fieldIdx[b]]
	end
	return nil
end
mt.__newindex = function(t,k,v)
	error('do not edit config')
end
mt.__metatable = false
for _,v in ipairs(data) do
	setmetatable(v,mt)
end
return data
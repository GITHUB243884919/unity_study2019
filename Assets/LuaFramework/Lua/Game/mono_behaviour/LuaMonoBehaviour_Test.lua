LuaMonoBehaviour_Test = {}

LuaMonoBehaviour_Test.gameObject = nil

LuaMonoBehaviour_Test.Awake = function(_gameObject)
    gameObject = _gameObject
    print('~~~~~~~~~~~~~~~int the lua file LuaMonoBehaviour_Test Awake' .. '   ' .. gameObject.name)
end

LuaMonoBehaviour_Test.Start = function()
    print('~~~~~~~~~~~~~~~int the lua file LuaMonoBehaviour_Test Start')
end

LuaMonoBehaviour_Test.OnDestroy = function()
    print('~~~~~~~~~~~~~~~int the lua file LuaMonoBehaviour_Test OnDestroy')
end


local gameObject
function Awake(_gameObject)
	gameObject = _gameObject
	print('LuaMono Awake' .. '   ' .. gameObject.name)
end

function Start()
	print('LuaMono Start')
end
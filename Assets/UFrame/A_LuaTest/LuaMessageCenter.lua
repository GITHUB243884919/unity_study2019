local gameObject
function RecvMessage(msg)
	print('RecvMessage ' .. msg.messageID .. "  " .. msg.field1)
end

function ttttt()

end

function Awake(_gameObject)
	gameObject = _gameObject
	print('LuaMono Awake' .. '   ' .. gameObject.name)
	--LuaMessageCenter.Instance.luaFuncs.Add(1000, RecvMessage)
	LuaMessageCenter.Instance.dictTest:Add(1000, 1000)
	--LuaMessageCenter.Instance.VOID_CB___S:Add(100, ttttt)
	--LuaMessageCenter.Instance.luaFuncs = RecvMessage
	LuaMessageCenter.Instance:AddMessage(1000, RecvMessage)
	
	
	local msg = LuaMessageCenter.MessageTest.New()
	msg.messageID = 1001
	msg.field1 = 2000
	LuaMessageCenter.Instance:SendMessage(1001, msg)
	
end

function Start()
	print('LuaMono Start')
end


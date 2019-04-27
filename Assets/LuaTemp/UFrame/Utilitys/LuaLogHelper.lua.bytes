-- Lua端的Log助手

local rawprint = print
local rawerror = error
local logHelper = {}

local isLog = true  -- 是否打印日志
local tablePritDepth = 5 --table最深的打印层次

-- 普通日志
function logHelper.debug(...)
	if LogFunction then
		LogFunction(3,...)
	end
end

-- 警告
function logHelper.warn(...)
	if LogFunction then
		LogFunction(2,...)
	end
end

-- 错误
function logHelper.error(...)
	if LogFunction then
		LogFunction(0,...)
	end
end

-- 初始化
function logHelper.initialize()

end

-- 函数注册到全局
rawset(_G, "print", logHelper.debug)
rawset(_G, "warn", logHelper.warn)
rawset(_G, "error", logHelper.error)
rawset(_G, "rawprint", rawprint)
rawset(_G, "rawerror", rawerror)

return logHelper
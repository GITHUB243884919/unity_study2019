config = {};local m = config;local g = _G;local rawget = rawget;local rawset = rawset;_ENV = setmetatable({},{__index = function(t,k) return rawget(m,k) or rawget(g,k) end,__newindex = m});
config_test_sheet1 = require "test_sheet1"
config_sheet1 = require "sheet1"

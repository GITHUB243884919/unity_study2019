---
--- 全局变量的一些检查与控制
---

setmetatable(_G, {
    -- 控制新建全局变量
    __newindex = function(_, k)
        error("attempt to add a new value to global,key: " .. k, 2)
    end,

    -- 控制访问全局变量
    __index = function(_, k)
        error("attempt to index a global value,key: "..k,2)
    end
})

function CreateEnumTable(tbl, index)
    local enumtbl = {}
    local enumindex = index or 0
    for i, v in ipairs(tbl) do
        enumtbl[v] = enumindex + i
    end
    return enumtbl
end


UIDepth = {
    BOTTOMMOST = 1, -- 最下面的一层
    BOTTOM = 2, -- 较低的层级
    NORMAL = 3, -- 正常的层级
    TOP = 4, -- 顶层
    TOPMOST = 5, -- 最顶层，显示在最上面
    DEBUG = 6, -- 用于调试的层级
}

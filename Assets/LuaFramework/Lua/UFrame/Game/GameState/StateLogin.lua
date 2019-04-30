

StateLogin = {}
function StateLogin:Enter()
    print("lua StateLogin Enter")
    --加载Login场景
    --ResHelper.LoadScene("scenes/login")
    SceneManagement.GetInstance():LoadScene("scenes/login", function ()
        --打开Login的UI
        print("lua open login ui")
        EventManager.Instance():DispatchEvent(MessageCode.UIMsg_CreateUI, Enum_UINameDefine.UI_Login)
    end)
end


--Class = require("UFrame/Core/middleclass")
--StateLogin = Class.class('StateLogin')
--
--function StateLogin.Enter()
--    print("lua StateLogin Enter")
--end
--
--StateLogin = StateLogin:new()








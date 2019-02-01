using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
public class LuaMessageCenter : MonoBehaviour {
    protected LuaState luaState = null;
    public class Message
    {
        public int messageID;
    }

    public class MessageTest : Message
    {
        public int field1;
    }

    public delegate void VOID_MESSAGE(Message msg);

    public delegate void VOID_CB___();

    public static LuaMessageCenter Instance;

    //public Dictionary<int, LuaFunction> luaFuncs = new Dictionary<int, LuaFunction>();
    public Dictionary<int, VOID_MESSAGE> luaFuncss = new Dictionary<int, VOID_MESSAGE>();
    public VOID_MESSAGE luaFuncs = null;
    public Dictionary<int, int> dictTest = new Dictionary<int, int>();
    public Dictionary<int, VOID_CB___> VOID_CB___S = new Dictionary<int, VOID_CB___>();
    public Queue<Message> messages = new Queue<Message>();

    public void AddMessage(int id, VOID_MESSAGE cb)
    {
        luaFuncss.Add(id, cb);
    }

    public void SendMessage(int id, Message msg)
    {
        VOID_MESSAGE cb = null;
        luaFuncss.TryGetValue(id, out cb);
        cb(msg);

    }
    


    private void Awake()
    {
        AddMessage(1001, CSCallback);

        VOID_CB___S.Add(1, Awake);
        Instance = this;
        Init();
        //luaFuncs.Add()
        luaState.DoFile(name);
        //调用lua注册函数，该函数调用消息中心注册
        LuaFunction func = luaState.GetFunction("Awake");

        func.BeginPCall();
        func.PushGeneric<GameObject>(gameObject);
        func.PCall();
        func.EndPCall();

        func.Dispose();
        func = null;

        
    }

    // Use this for initialization
    void Start()
    {
        LuaFunction func = luaState.GetFunction("Start");
        func.Call();
        func.Dispose();
        func = null;

        MessageTest msg = new MessageTest();
        msg.messageID = 1000;
        msg.field1 = 2000;
        messages.Enqueue(msg);

    }

    // Update is called once per frame
    void Update()
    {
        while (true)
        {
            if (messages.Count <= 0)
            {
                return;
            }

            Message msg = messages.Dequeue();
            //LuaFunction func = null;
            //if (luaFuncs.TryGetValue(msg.messageID, out func))
            //{
            //    func.BeginPCall();
            //    func.PushGeneric<Message>(msg);
            //    func.PCall();
            //    func.EndPCall();

            //    func.Dispose();
            //    func = null;
            //}

            //VOID_MESSAGE func = null;
            //if (luaFuncs.TryGetValue(msg.messageID, out func))
            //{
            //    func(msg);

            //}

            //luaFuncs(msg);

            //VOID_MESSAGE func = null;
            //if (luaFuncss.TryGetValue(msg.messageID, out func))
            //{
            //    func(msg);

            //}

        }
    }

    void OnDestroy()
    {
        luaState.CheckTop();
        luaState.Dispose();
        luaState = null;
    }

    protected void OpenLibs()
    {
        luaState.OpenLibs(LuaDLL.luaopen_pb);
        luaState.OpenLibs(LuaDLL.luaopen_struct);
        luaState.OpenLibs(LuaDLL.luaopen_lpeg);
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        luaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

        if (LuaConst.openLuaSocket)
        {
            //OpenLuaSocket();
        }

        if (LuaConst.openLuaDebugger)
        {
            //OpenZbsDebugger();
        }
    }

    void Init()
    {
        //InitLoader();
        luaState = new LuaState();
        OpenLibs();
        luaState.LuaSetTop(0);
        Bind();
        //LoadLuaFiles();

        string fullPath = Application.dataPath + "/UFrame/A_LuaTest";
        luaState.AddSearchPath(fullPath);

        luaState.Start();
    }

    void Bind()
    {
        LuaBinder.Bind(luaState);
        DelegateFactory.Init();
        LuaCoroutine.Register(luaState, this);
    }

    void CSCallback(Message msg)
    {
        MessageTest _msg = msg as MessageTest;
        Debug.Log("CSCallback " + +_msg.messageID + " " +  _msg.field1);

    }
}

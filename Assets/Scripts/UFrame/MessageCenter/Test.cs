using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;

using UFrameMessage = UFrame.MessageCenter.Message;

public class MessageTest : UFrameMessage
{
    public int field1;
    public int field2;
    #region pool
    public static ObjectPool<MessageTest> pool = new ObjectPool<MessageTest>();
    public override void OnDeathToPool()
    {
        base.OnDeathToPool();
        this.field1 = -1;
        this.field2 = -1;
    }
    public override void Release()
    {
        pool.Delete(this);

    }
    #endregion

    public void Init(int field1, int field2)
    {
        this.field1 = field1;
        this.field2 = field2;
    }
}

public class MessagetExecutorTest : IMessageExecutor
{
    public void Execute(UFrameMessage msg)
    {
        MessageTest _msg = msg as MessageTest;
        Debug.LogError(msg.messageID + " " + _msg.field1 + " " + _msg.field2);
    }
}

public class BroadcastMessageExecutorTest : BroadcastMessageExecutor
{
    public override void Execute(UFrameMessage msg)
    {
        MessageTest _msg = msg as MessageTest;
        Debug.LogError(msg.messageID + " " + _msg.field1 + " " + _msg.field2);
    }
}

public class Test : MonoBehaviour 
{

	// Use this for initialization
    DirectMessageCenter directMessageCenter;
    BroadcastMessageCenter broadcastMessageCenter;
	void Start () 
    {
        LinkedList<int> link = new LinkedList<int>();
        link.AddLast(100);
        link.AddLast(1000);
        link.Remove(100);



        directMessageCenter = new DirectMessageCenter();
        directMessageCenter.Regist(1000, new MessagetExecutorTest());

        broadcastMessageCenter = new BroadcastMessageCenter();
        broadcastMessageCenter.Regist(new BroadcastMessageExecutorTest());

        MessageTest msgTest1 = MessageTest.pool.New();
        msgTest1.messageID = 1000;
        msgTest1.Init(100, 200);

        MessageTest msgTest2 = MessageTest.pool.New();
        msgTest2.messageID = 1000;
        msgTest2.Init(300, 400);

        directMessageCenter.Send(msgTest1);
        broadcastMessageCenter.Send(msgTest2);
	}
	
	// Update is called once per frame
	void Update () 
    {
        directMessageCenter.Tick();
        broadcastMessageCenter.Tick();
	}

}

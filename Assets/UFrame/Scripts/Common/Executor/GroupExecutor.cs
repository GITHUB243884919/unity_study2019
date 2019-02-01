/*
Author:		Augustine
History:	2.5.2016 创建
note   :    组合执行器
 */

using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class CGroupExecutor : CExecutor
{
    public CGroupExecutor()
        :base()
    {
        linkList = new LinkedList<CExecutor>();
    }

    public virtual void AddExecutor(CExecutor childExecutor)
    {
        if (childExecutor.Parent == this)
        {
            return;
        }

        if (null != childExecutor.Parent)
        {
            childExecutor.Parent.RemoveExecutor(childExecutor);
        }
        linkList.AddLast(childExecutor.LinkNode);
        childExecutor.SetParent(this, false);
    }

    public virtual void RemoveExecutor(CExecutor childExecutor)
    {
        linkList.Remove(childExecutor.LinkNode);
        childExecutor.SetParent(null, false);
    }

    public override void Clear()
    {
        base.Clear();
        LinkedListNode<CExecutor> curNode = linkList.First;
        LinkedListNode<CExecutor> lastNode = linkList.Last;
        while (null != curNode)
        {
            // do
            curNode.Value.Clear();
            curNode.Value.SetParent(null, false);

            if (curNode == lastNode)
            {
                curNode = null;
            }
            else
            {
                curNode = curNode.Next;
            }
        }

        linkList.Clear();
    }

    public int ChildCount
    {
        get
        {
            return linkList.Count;
        }
    }

    public LinkedListNode<CExecutor> ChildFirst
    {
        get
        {
            return linkList.First;
        }
    }

    public LinkedListNode<CExecutor> ChildLast
    {
        get
        {
            return linkList.Last;
        }
    }


    protected LinkedList<CExecutor> linkList;

}






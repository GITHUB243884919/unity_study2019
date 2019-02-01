using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFrame.Executor
{
    public abstract class GroupExecutor : ExecutorCondition
    {
        public LinkedList<Executor> linkList = new LinkedList<Executor>();

        public void AddExecutor(Executor executor, bool immediately = false)
        {
            linkList.AddLast(executor.linkNode);
            executor.parent = this;
            if (immediately)
            {
                executor.Play();
            }
        }

        public void RemoveExecutor(Executor executor)
        {
            linkList.Remove(executor.linkNode);
        }

        public void RemoveAllExecutors()
        {
            linkList.Clear();
        }
    }

}


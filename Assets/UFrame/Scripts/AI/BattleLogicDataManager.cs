using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
namespace GameName.Battle.Logic.Data
{
    public class BattleLogicDataManager : Singleton<BattleLogicDataManager>
    {
        Dictionary<int, Tank> tanks = new Dictionary<int, Tank>();


        public void AddTank(Tank tank)
        {
            tanks.Add(tank.GetID(), tank);
        }
    }

    
}


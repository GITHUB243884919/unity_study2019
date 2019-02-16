using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
namespace GameName.Battle.Logic.Data
{
    public class BattleLogicDataManager : Singleton<BattleLogicDataManager>
    {
        Dictionary<int, Tank> tanks = new Dictionary<int, Tank>();
        Dictionary<int, Player> players = new Dictionary<int, Player>();

        public void AddTank(Tank tank)
        {
            tanks.Add(tank.GetID(), tank);
        }

        public void AddPlayer(Player player)
        {
            players.Add(player.ID, player);
        }
    }

    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.Common;
using UFrame.AI;
namespace GameName.Battle.Logic.Data
{
    public class BattleLogicDataManager
    {
        Dictionary<int, Tank> tanks = new Dictionary<int, Tank>();
        Dictionary<int, Player> players = new Dictionary<int, Player>();
        Dictionary<int, SimpleMoveObjectCtr> tankCtrs = new Dictionary<int, SimpleMoveObjectCtr>();
        List<Avoidance> avoidances = new List<Avoidance>();

        public void AddTank(Tank tank)
        {
            tanks.Add(tank.GetID(), tank);
        }

        public Dictionary<int, Tank> GetTanks()
        {
            return tanks;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player.ID, player);
        }

        public Dictionary<int, Player> GetPlayers()
        {
            return players;
        }

        public void AddTankCtr(SimpleMoveObjectCtr ctr)
        {
            tankCtrs.Add(ctr.moveObject.GetID(), ctr);
        }

        public Dictionary<int, SimpleMoveObjectCtr> GetTankCtrs()
        {
            return tankCtrs;
        }

        public void AddAvoidance(Avoidance av)
        {
            avoidances.Add(av);
        }

        public List<Avoidance> GetAvoidances()
        {
            return avoidances;
        }


    }


}


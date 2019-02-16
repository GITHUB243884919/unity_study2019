using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.AI;
namespace GameName.Battle.Logic.Data
{
    public class Tank : MoveObject
    {
        public int tankType;
        public bool isPlayer;
        public bool isCaption;
    }


    public class Player
    {
        static int sID;
        public int ID;
        public List<Tank> tanks;
        public bool isSelf;
        public Player()
        {
            tanks = new List<Tank>();
            ID = sID++;
        }
    }

}



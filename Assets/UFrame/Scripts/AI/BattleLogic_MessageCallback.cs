using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame.MessageCenter;
using GameName.Lua.Config;
using FixMath;

namespace GameName.Battle.Logic
{
    public partial class BattleLogic : IMessageExecutor
    {
        void OnD2L_BattleInit(UFrame.MessageCenter.Message msg)
        {
            D2L_BattleInit convMsg = msg as D2L_BattleInit;
            if (convMsg.result)
            {
                displayOK = true;
                Debug.LogError("display init ok");
            }

        }

        void OnJOY_Press(UFrame.MessageCenter.Message msg)
        {
            JOY_Press convMsg = msg as JOY_Press;
            var tankCtrs = logicDataManager.GetTankCtrs();
            var tankCtr = tankCtrs[convMsg.tankID];
            tankCtr.moveObject.couldMove = convMsg.couldMove;
            tankCtr.moveObject.couldTurn = convMsg.couldTurn;

            if (convMsg.couldMove && convMsg.couldTurn)
            {
                tankCtr.Turn(F64Vec3.FromUnityVector3(convMsg.dir));
            }
        }
    }
}

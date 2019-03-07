using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMTest : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.LogError(stateInfo.fullPathHash);
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
}

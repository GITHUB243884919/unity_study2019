using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEasyTouch : MonoBehaviour
{
    public ETCJoystick joy;
    void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
        //ETCJoystick.OnMoveStartHandler + 
        //joy.OnDownUp.AddListener(OnUp);
        //joy.OnDownDown.AddListener(OnDown);
        //joy.OnDownLeft.AddListener(OnLeft);
        //joy.OnDownRight.AddListener(OnRight);
        //joy.onTouchStart.AddListener(OnTouchStart);
        //joy.onTouchUp.AddListener(OnTouchUp);

        //joy.onMoveStart.AddListener(OnMoveStart);
        //joy.onMove.AddListener(OnMove);
        //joy.onMoveSpeed.AddListener(OnMoveSpeed);
        //joy.onMoveEnd.AddListener(OnMoveEnd);
        joy.OnPressUp.AddListener(OnUp);
        joy.OnPressDown.AddListener(OnDown);
        joy.OnPressLeft.AddListener(OnLeft);
        joy.OnPressRight.AddListener(OnRight);

    }
    
    void OnDisable()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
    }
    void OnDestroy()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
    }
    // Touch start event
    public void On_TouchStart(Gesture gesture)
    {
        Debug.Log("Touch in " + gesture.position);
    }

    public void OnPressLeftHandler(Gesture gesture)
    {
        Debug.Log("Touch in " + gesture.position);
    }

    public void OnUp()
    {
        Debug.LogError("OnUp");
        PrintXY();
    }

    public void OnDown()
    {
        Debug.LogError("OnDown");
        PrintXY();
    }

    public void OnLeft()
    {
        Debug.LogError("OnLeft");
        PrintXY();
    }

    public void OnRight()
    {
        Debug.LogError("OnRight");
        PrintXY();
    }

    public void OnTouchStart()
    {
        Debug.LogError("OnTouchStart");
        PrintXY();
    }

    public void OnTouchUp()
    {
        Debug.LogError("OnTouchUp");
        PrintXY();
    }

    public void OnMoveStart()
    {
        Debug.LogError("OnMoveStart");
        PrintXY();
    }
    public void OnMove()
    {
        Debug.LogError("OnMove");
        PrintXY();
    }
    public void OnMoveSpeed()
    {
        Debug.LogError("OnMoveSpeed");
        PrintXY();
    }
    public void OnMoveEnd()
    {
        Debug.LogError("OnMoveEnd");
        PrintXY();
    }


    void PrintXY()
    {
        float h = joy.axisX.axisValue;
        float v = joy.axisY.axisValue;
        Debug.LogError(h + " " + v);
    }

}

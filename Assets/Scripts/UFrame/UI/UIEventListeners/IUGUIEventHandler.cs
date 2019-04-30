using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void UIEventHandler(GameObject obj);
public delegate void UIDragEventHandlerDetail(GameObject obj, Vector2 deltaPos, Vector2 curToucPosition);
public delegate void StrValueChangeAction(GameObject obj, string text);
public delegate void FloatValueChangeAction(GameObject obj, float value);
public delegate void IntValueChangeAction(GameObject obj, int value);
public delegate void BoolValueChangeAction(GameObject obj, bool isSelect);
public delegate void RectValueChangeAction(GameObject obj, Vector2 rect);

/// <summary>
/// UGUI事件的处理容器
/// </summary>
public interface IUGUIEventHandler
{
    /// <summary>
    /// 注册UIEventListener
    /// </summary>
    /// <param name="obj"></param>
    void AttachListener(GameObject obj);

    /// <summary>
    /// 反注册UIEventListener
    /// </summary>
    /// <param name="obj"></param>
    void UnAttachListener(GameObject obj);

    /// <summary>
    /// 移除GameObject上面的EventListener
    /// </summary>
    /// <param name="obj"></param>
    void RemoveEventHandler(GameObject obj);
}

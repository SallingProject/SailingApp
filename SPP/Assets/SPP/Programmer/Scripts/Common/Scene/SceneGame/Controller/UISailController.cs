/**************************************************************************************/
/*! @file   UIHandleController.cs
***************************************************************************************
@brief      セイルを動かすコントローラークラス
***************************************************************************************
@author     Ko Hashimoto and Kana Yoshidumi
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class UISailController : BaseObject {

    [SerializeField]
    Image m_root;

    [SerializeField]
    Image m_controllObject;

    [SerializeField]
    RectTransform m_right;

    [SerializeField]
    RectTransform m_left;

    public bool mIsDown
    {
        get;
        private set;
    }
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        EventTrigger trigger = m_controllObject.GetComponent<EventTrigger>();

        // PointerDownイベントの追加
        {
            EventTrigger.Entry down = new EventTrigger.Entry();
            down.eventID = EventTriggerType.PointerDown;
            down.callback.RemoveAllListeners();
            down.callback.AddListener(data => mIsDown = true);
            trigger.triggers.Add(down);
        }

        // PointerUpイベントの追加
        {
            EventTrigger.Entry up = new EventTrigger.Entry();
            up.eventID = EventTriggerType.PointerUp;
            up.callback.RemoveAllListeners();
            up.callback.AddListener(data => mIsDown = false);
            trigger.triggers.Add(up);
        }

        // Dragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.RemoveAllListeners();
            drag.callback.AddListener(Drag);
            trigger.triggers.Add(drag);
        }
    }

    public override void mOnUpdate()
    {
        base.mOnUpdate();

    }


    void Drag(BaseEventData eventData)
    {

        var touch = InputManager.mInstance.mGetTouchInfo();
        if (touch.mLocalDeltaPosition.x > 0)
        {
            if (m_controllObject.rectTransform.position.x + touch.mLocalDeltaPosition.x < m_right.position.x)
            {
                m_controllObject.rectTransform.position += new Vector3(touch.mLocalDeltaPosition.x, 0, 0);
            }
        }
        else 
        {
            if (m_controllObject.rectTransform.position.x + touch.mLocalDeltaPosition.x > m_left.position.x )
            {
                m_controllObject.rectTransform.position += new Vector3(touch.mLocalDeltaPosition.x, 0, 0);
            }
        }

    }
}

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

    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        EventTrigger trigger = m_controllObject.GetComponent<EventTrigger>();
        
        // Dragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
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

        var touch = InputManager.mInstance.mGetTouchInfo(0);
        if (touch._deltaPosition.x > 0)
        {
            if (m_controllObject.rectTransform.position.x + touch._deltaPosition.x < m_right.position.x)
            {
                m_controllObject.rectTransform.position += new Vector3(touch._deltaPosition.x * touch._speed, 0, 0);
            }
        }
        else 
        {
            if (m_controllObject.rectTransform.position.x + touch._deltaPosition.x > m_left.position.x )
            {
                m_controllObject.rectTransform.position += new Vector3(touch._deltaPosition.x * touch._speed, 0, 0);
            }
        }

    }
}

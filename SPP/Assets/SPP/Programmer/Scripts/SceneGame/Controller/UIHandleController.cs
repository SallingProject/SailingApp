﻿/**************************************************************************************/
/*! @file   UIHandleController.cs
***************************************************************************************
@brief      船を動かすメインハンドルコントローラークラス
***************************************************************************************
@author     Ko Hashimoto and Kana Yoshidumi
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/**************************************************************************************
@brief  船を動かすメインハンドルコントローラークラス
*/
public class UIHandleController : BaseObject{

    [SerializeField]
    private float m_maxZRotation = 80;      // 回転する最大値

        
    [SerializeField]
    private RectTransform m_handle;         // 回転するオブジェクト

    [SerializeField]
    private Image m_clickImage;             // ドラッグや、MouseUp,MouseDownを受け付けるコンポーネント


    /**************************************************************************************
    @brief  現在のハンドルのZ方向の傾きを取得  
    */
    public float mHandleRotationZ
    {
        get
        {
            // 左側の場合
            if((int)m_handle.localEulerAngles.z >= 0 && (int)m_handle.localEulerAngles.z <= m_maxZRotation)
            {
                return m_handle.localEulerAngles.z;
            }// 右側の場合
            else if (m_handle.localEulerAngles.z >= 0 || m_handle.localEulerAngles.z > 360 - m_maxZRotation)
            {
                return m_handle.localEulerAngles.z - 360;
            }
            
            return 0.0f;
        }
    }

    /**************************************************************************************
    @brief  現在ドラッグ中かのフラグ
    @note   押されている: true / 離された : false
    */
    public bool mIsDown { get; private set; }

    private Vector2 m_dragPosition = Vector2.zero; // ドラッグ開始地点保存用
    private bool m_isLeftHandle = false;           // ハンドルが左側にあるときtrue/右側のときにfalse

    /**************************************************************************************
    @brief  BaseObjectの実装。タイミングはAwake
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        // OnUpdateなどは使わないのでBaseObjectの管理から外す
        mUnregisterList(this);

        EventTrigger trigger = m_clickImage.GetComponent<EventTrigger>();
        // PointerEnterイベントの追加
        {
            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerDown;
            enter.callback.AddListener(Down);
            trigger.triggers.Add(enter);
        }

        // PointerExitイベントの追加
        {
            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerUp;
            exit.callback.AddListener(Up);
            trigger.triggers.Add(exit);
        }

        // Dragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.AddListener(Drag);
            trigger.triggers.Add(drag);
        }
    }

    /**************************************************************************************
    @brief  指定のオブジェクトを押された時に1度呼ばれる
    */
    void Down(BaseEventData eventData)
    {

#if UNITY_EDITOR || UNITY_WINDOWS
        mIsDown = true;
        m_dragPosition = Input.mousePosition;
#elif UNITY_ANDROID
        
        if (Input.touchCount > 0)
        {
            mIsDown = true;
            foreach (Touch t in Input.touches)
            {
                m_dragPosition = t.position; 
                mIsDown = true;
        
                break;
            }
        }
#endif
    }


    /**************************************************************************************
    @brief  指定のオブジェクトを押された状態から離された時に1度呼ばれる
    */
    void Up(BaseEventData eventData)
    {
        mIsDown = false;
        m_isLeftHandle = ((int)m_handle.localEulerAngles.z >= 0 && (int)m_handle.localEulerAngles.z <= m_maxZRotation) ? true : false;
        StartCoroutine(ResetHandle());
    }

    /**************************************************************************************
    @brief  指定のオブジェクトがドラッグ状態のときに呼ばれる
    */
    void Drag(BaseEventData eventData)
    {
        Vector2 mousePosition = Vector2.zero;
        Vector2 diff = Vector2.zero;
#if UNITY_EDITOR || UNITY_WINDOWS

        mousePosition = Input.mousePosition;
        diff = m_dragPosition - mousePosition;

#elif UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled)
                {
        
                    mousePosition = t.position;

                    diff = -t.deltaPosition;
                }
                break;
            }
        }
#endif
      
        // 左
        if (diff.x > 0)
        {
            if (mHandleRotationZ +diff.x  < m_maxZRotation)
            {
                m_handle.localEulerAngles += new Vector3(0, 0, diff.x);
            }
        }
        else // 右
        {
            if (mHandleRotationZ + diff.x > -m_maxZRotation)
            {
                m_handle.localEulerAngles += new Vector3(0, 0, diff.x);
            }
        }

        
        m_dragPosition = Input.mousePosition;
    }

    /**************************************************************************************
    @brief  ハンドルコントローラーの傾きを0に戻すコルーチン
    */
    IEnumerator ResetHandle()
    {
        // 戻す量
        float resetValue = 0.0f;
        if (m_isLeftHandle)
        {
            resetValue = m_handle.localEulerAngles.z;
        }
        else
        {
            resetValue = (m_handle.localEulerAngles.z - 360);
        }

        int LoopMax = 5; // テキトーな値
        for(int i = 0; i < LoopMax; ++i)
        {
            m_handle.localEulerAngles -= new Vector3(0, 0, resetValue / LoopMax);
            yield return null;
        }
        
        // 念のため
        m_handle.localEulerAngles = new Vector3(0, 0, 0);
    }
}

/**************************************************************************************/
/*! @file   InputManager.cs
***************************************************************************************
@brief      入力処理などはここから取得する
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/**************************************************************************************
@brief  	タッチ情報のクラス
*/
public interface ITouchInfo
{
    Vector2 mPosition { get; }
    Vector2 mDeltaPosition { get; }

    int mFingerId { get; }

    TouchPhase mTouchPhase { get; }

    bool mUsed { get; }
}



/**************************************************************************************
@brief  	タッチ情報のクラス
*/
public class TouchInfo : ITouchInfo
{
    public static readonly int InvalidFingerId = -1;
    public Vector2 _position = Vector2.zero;
    public int _fingerId = -1;
    public Vector2 _prevPosition = Vector2.zero;
    public TouchPhase _phase;
    public bool _used = false;

    public Vector2 mPosition { get { return _position; } }

    public Vector2 mDeltaPosition { get { return (_position - _prevPosition); } }

    public int mFingerId { get { return _fingerId; } }

    public TouchPhase mTouchPhase { get { return _phase; } }

    public bool mUsed { get { return _used; } }

    public void Clear()
    {
        _position = Vector2.zero;
        _prevPosition = Vector2.zero;
        _phase = TouchPhase.Ended;
        _fingerId = InvalidFingerId;
        _used = false;
    }
}

/**************************************************************************************
@brief  	管理クラス
*/
public class InputManager : BaseObjectSingleton<InputManager> {

    [SerializeField]
    int m_maxTouchCount = 1;

    public int mTouchCount
    {
        get { return Input.touchCount; }
    }

    List<TouchInfo> m_touchBuffer = new List<TouchInfo>();
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        // 指定数分のバッファを作成
        for(int i = 0; i < m_maxTouchCount; ++i)
        {
            TouchInfo info = new TouchInfo();
            info.Clear();
            m_touchBuffer.Add(info);
        }
    }

    /**************************************************************************************
    @brief  	更新処理
    */
    public override void mOnFastUpdate()
    {
        base.mOnFastUpdate();


#if UNITY_EDITOR || UNITY_WINDOWS

        m_touchBuffer[0]._prevPosition      = m_touchBuffer[0]._position;
        m_touchBuffer[0]._position          = Input.mousePosition;
        m_touchBuffer[0]._used              = false;

        if (Input.GetKeyDown(DebugManager.mInstance.mkConsoleCommandKey))
        {
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }

#elif UNITY_ANDROID || UNITY_IOS

        int kMaxLoop = (m_maxTouchCount < Input.touchCount) ? m_maxTouchCount : Input.touchCount;
        for(int i = 0; i < kMaxLoop; ++i)
        {
            bool isRegistered = false;
            // 前回のを追跡する処理
            foreach (var index in m_touchBuffer)
            {
                if (index._fingerId == Input.touches[i].fingerId)
                {
                    index._prevPosition = index._position;
                    index._position = Input.touches[i].position;
                    index._phase = Input.touches[i].phase;
                    index._used = false;
                    isRegistered = true;
                }
            }

            // 新規登録
            if (!isRegistered)
            {
                m_touchBuffer[i]._prevPosition  = m_touchBuffer[i]._position;
                m_touchBuffer[i]._position      = Input.touches[i].position;
                m_touchBuffer[i]._phase         = Input.touches[i].phase;
                m_touchBuffer[i]._fingerId      = Input.touches[i].fingerId;
                m_touchBuffer[i]._phase         = Input.touches[i].phase;
                m_touchBuffer[i]._used          = false;
            }
        }
        
        if (Input.touchCount == m_maxTouchCount)
        {
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }
#endif
    }

    /**************************************************************************************
    @brief  	インデックスから取得可能
    */
    public ITouchInfo mGetTouchInfo(int id)
    {
#if UNITY_EDITOR || UNITY_WINDOWS
        return m_touchBuffer[0];
#elif UNITY_ANDROID || UNITY_IOS
        return m_touchBuffer[id];
#endif
    }

    /**************************************************************************************
     @brief  	まだつか割れていないのを取得
    */
    public ITouchInfo mGetTouchInfo()
    {
#if UNITY_EDITOR || UNITY_WINDOWS
        return m_touchBuffer[0];
#elif UNITY_ANDROID || UNITY_IOS
        foreach (var index in m_touchBuffer)
        {
            if (!index._used)
            {
                index._used = true;
                return index;
            }
        }
        
        return null;
#endif
    }

}

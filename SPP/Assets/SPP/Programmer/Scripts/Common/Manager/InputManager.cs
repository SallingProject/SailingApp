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
@brief  	タッチの状態列挙
*/
public enum ETouchType
{
    Begin = 0,   // 押し始め
    Flick,       // フリック
    Swipe,       // スワイプ
    Cancel,      // キャンセル
    Stationary,  // 入力禁止
    End,         // 離した時
}

/**************************************************************************************
@brief  	タッチ情報のインターフェースクラス
*/
public interface ITouchInfo
{
    Vector2 mPosition { get; }
    Vector2 mDeltaPosition { get; }

    int mFingerId { get; }

    ETouchType mTouchType { get; }

    bool mUsed { get; }
}



/**************************************************************************************
@brief  	タッチ情報のクラス
*/
public class TouchInfo : ITouchInfo
{
    public static readonly int InvalidFingerId = -1;
    public int _fingerId = InvalidFingerId;         // フィンガーID  
    public Vector2 _position = Vector2.zero;        // 現在のポジション情報
    public Vector2 _prevPosition = Vector2.zero;    // 前回のポジション情報
    public Vector2 _beginPosition = Vector2.zero;   // 
    public Vector2 _beginDiff = Vector2.zero;       // 押し始めから現在までの差分
    public float _touchTime = 0.0f;                 // 押されたときからの計測時間   
    public ETouchType _touchType;                   // 押されている状態
    public bool _used = false;                      // 現在使われているか
    
    public Vector2 mPosition { get { return _position; } }

    public Vector2 mDeltaPosition { get { return (_position - _prevPosition); } }

    public int mFingerId { get { return _fingerId; } }

    public ETouchType mTouchType { get { return _touchType; } }

    public bool mUsed { get { return _used; } }

    public void Clear()
    {
        _position       = Vector2.zero;
        _prevPosition   = Vector2.zero;
        _beginPosition  = Vector2.zero;
        _touchTime      = 0.0f;
        _touchType      = ETouchType.End;
        _fingerId       = InvalidFingerId;
        _used           = false;
    }
}

/**************************************************************************************
@brief  	管理クラス
*/
public class InputManager : BaseObjectSingleton<InputManager> {

    public static readonly float kFlickTime = 0.5f;
    public static readonly float kFlickMagnitude = 10f;

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

        int kMaxLoop = (m_maxTouchCount < Input.touchCount) ? m_maxTouchCount : Input.touchCount;
        for (int i = 0; i < kMaxLoop; ++i)
        {
            bool isRegistered = false;
            // 前回のを追跡する処理
            foreach (var index in m_touchBuffer)
            {
                if (mTouchUpdate(index, i))
                {
                    isRegistered = true;
                    break;
                }
            }

            // 新規登録処理
            if (!isRegistered)
            {
                m_touchBuffer[i]._fingerId = Input.touches[i].fingerId;
                mTouchUpdate(m_touchBuffer[i], i);
            }
        }

        if (Input.touchCount == m_maxTouchCount)
        {
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }

#if UNITY_EDITOR || UNITY_WINDOWS

        mMouseUpdate();

        if (Input.GetKeyDown(DebugManager.mInstance.mkConsoleCommandKey))
        {
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }

#endif
    }

    /**************************************************************************************
    @brief  	マウス時の更新処理
    */
    private void mMouseUpdate()
    {
        // 基本的な更新処理
        m_touchBuffer[0]._prevPosition = m_touchBuffer[0]._position;
        m_touchBuffer[0]._position = Input.mousePosition;
        m_touchBuffer[0]._used = false;

        // 押したとき
        if (Input.GetMouseButtonDown(0))
        {
            m_touchBuffer[0]._beginPosition = Input.mousePosition;
            m_touchBuffer[0]._touchType = ETouchType.Begin;
        }

        // 押している間
        if (Input.GetMouseButton(0))
        {
            //時間計測開始
            m_touchBuffer[0]._touchTime += Time.deltaTime;

            Vector2 currentTapPoint = m_touchBuffer[0]._position;
            m_touchBuffer[0]._beginDiff = (currentTapPoint - m_touchBuffer[0]._beginPosition);
            if (m_touchBuffer[0]._beginDiff.magnitude > kFlickMagnitude / 2)
            {
                // フリックしないときの状態を登録
                m_touchBuffer[0]._touchType = ETouchType.Swipe;
            }
        }

        // 押すのをやめたとき
        if (Input.GetMouseButtonUp(0))
        {
            m_touchBuffer[0]._touchType = ETouchType.End;

            //フリック条件。時間経過は0.5以下でmagnitudeが10以上なら
            if (m_touchBuffer[0]._touchTime <= kFlickTime && m_touchBuffer[0]._beginDiff.magnitude >= kFlickMagnitude)
            {
                m_touchBuffer[0]._touchType = ETouchType.Flick;
            }

            //最後にタイマーを初期化
            m_touchBuffer[0]._touchTime = 0f;
        }
    }

    /**************************************************************************************
    @brief  	タッチの更新処理
    */
    private bool mTouchUpdate(TouchInfo touch,int id)
    {
        // IDが違ったらと無視
        if (touch._fingerId != Input.touches[id].fingerId)
            return false;
        
        touch._prevPosition = m_touchBuffer[id]._position;
        touch._position = Input.touches[id].position;
        touch._used = false;
        var phase = Input.touches[id].phase;
        switch (phase)
        {
            case TouchPhase.Began:
                touch._beginPosition = Input.mousePosition;
                touch._touchType = ETouchType.Begin;
                break;

            case TouchPhase.Moved:
                //時間計測開始
                touch._touchTime += Time.deltaTime;

                Vector2 currentTapPoint = touch._position;
                touch._beginDiff = (currentTapPoint - touch._beginPosition);
                if (touch._beginDiff.magnitude > kFlickMagnitude / 2)
                {
                    // フリックしないときの状態を登録
                    touch._touchType = ETouchType.Swipe;
                }
                break;

            case TouchPhase.Ended:
                touch._touchType = ETouchType.End;

                //フリック条件。時間経過は0.5以下でmagnitudeが10以上なら
                if (touch._touchTime <= kFlickTime && touch._beginDiff.magnitude >= kFlickMagnitude)
                {
                    touch._touchType = ETouchType.Flick;
                }

                touch._touchTime = 0f;
                break;

            case TouchPhase.Stationary:
            case TouchPhase.Canceled:
                touch._touchType = (phase == TouchPhase.Canceled) ? ETouchType.Cancel : ETouchType.Stationary;
                touch._touchTime = 0f;
                break;
        }

        return true;
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

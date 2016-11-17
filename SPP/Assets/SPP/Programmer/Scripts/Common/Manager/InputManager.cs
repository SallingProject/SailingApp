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


public class TouchInfo
{
    public Vector2 _position = Vector2.zero;
    public Vector2 _deltaPosition = Vector2.zero;

    public float _speed = float.NaN;
    
    public void Clear()
    {
        _position = Vector2.zero;
        _deltaPosition = Vector2.zero;
        _speed = float.NaN;
    }
}

public class InputManager : BaseObjectSingleton<InputManager> {

    [SerializeField]
    int m_maxTouchCount = 1;

    [SerializeField]
    int m_addSpeed = 10;
    
    List<TouchInfo> m_touchBuffer = new List<TouchInfo>();
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }

    /**************************************************************************************
    @brief  	更新処理
    */
    public override void mOnFastUpdate()
    {
        base.mOnFastUpdate();

        // 一回一回履歴をクリア
        m_touchBuffer.Clear();

#if UNITY_EDITOR || UNITY_WINDOWS
        TouchInfo touchInfo = new TouchInfo();
        touchInfo._deltaPosition    = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        touchInfo._position         = Input.mousePosition;
        touchInfo._speed = m_addSpeed;
        m_touchBuffer.Add(touchInfo);

        if (Input.GetKeyDown(DebugManager.mInstance.mkConsoleCommandKey))
        {
            DebugManager.mInstance.IsConsoleOpen = true;
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }

#elif UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0 && Input.touchCount <= m_maxTouchCount)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
                    TouchInfo touchInfo         = new TouchInfo();
        
                    touchInfo._deltaPosition    = touch.deltaPosition;
                    touchInfo._position         = touch.position;
                    touchInfo._time             = Time.time;
                    touchInfo._deltaTime        = touch.deltaTime;
                    touchInfo._speed = m_addSpeed;

                    m_touchBuffer.Add(touchInfo);
				}
				break;
			}
		}

        if(Input.touchCount == m_maxTouchCount)
        {
            DebugManager.mInstance.IsConsoleOpen = true;
            DebugManager.mInstance.OpenDebugCommandKeyboard();
        }
#endif
    }

    /**************************************************************************************
    @brief  	インデックスから取得可能
    */
    public TouchInfo mGetTouchInfo(int id)
    {
#if UNITY_EDITOR || UNITY_WINDOWS
        return m_touchBuffer[0];
#elif UNITY_ANDROID || UNITY_IOS
        return m_touchBuffer[id];
#endif
    }


}

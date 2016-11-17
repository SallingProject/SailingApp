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

    public float _time = float.NaN;
    public float _deltaTime = float.NaN;

    public void Clear()
    {
        _position = Vector2.zero;
        _deltaPosition = Vector2.zero;
        _time = float.NaN;
        _deltaTime = float.NaN;
    }
}

public class InputManager : BaseObjectSingleton<InputManager> {

    [SerializeField]
    int m_maxTouchCount = 1;

    [SerializeField]
    float m_screenSize = 1080f;

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
        TouchInfo touch = new TouchInfo();
        DebugManager.mInstance.OutputMsg(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        touch._deltaPosition    = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        touch._position         = Input.mousePosition / m_screenSize;
        touch._deltaTime        = Time.deltaTime;
        touch._time             = Time.time;
        m_touchBuffer.Add(touch);

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

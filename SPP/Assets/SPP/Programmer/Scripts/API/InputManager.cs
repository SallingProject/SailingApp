/**************************************************************************************/
/*! @file   InputManager.cs
***************************************************************************************
@brief      入力処理などはここから取得する
***************************************************************************************
@author     Ko Hashimoto and Kana Yoshidumi
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : BaseObjectSingleton<InputManager> {
	public class TouchPosition{
		public float X {
			get;
			set;
		}

		public float Y {
			get;
			set;
		}
	}

	TouchPosition m_mouseDiff = new TouchPosition();
	/**************************************************************************************
    @brief  	タッチされた場所の座標を返す
    @param[in]	同時に何本指タッチまで許容するかの設定（スマートフォンのみに関係）
    @return 	Editor上は必ずサイズは１
    			スマートフォンのサイズはmaxTouchCountと同じ
    */
	public List<TouchPosition> GetPosition(int maxTouchCount)
	{

		List<TouchPosition> positionList = new List<TouchPosition> ();
		TouchPosition position = new TouchPosition ();
		#if UNITY_EDITOR || UNITY_WINDOWS
		position.X = Input.mousePosition.x;
		position.Y = Input.mousePosition.y;
		m_mouseDiff = position;
		positionList.Add(position);

		#elif UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0 && Input.touchCount <= maxTouchCount)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					position.X = touch.position.x;
					position.Y = touch.position.y;
					positionList.Add(position);
				}
				break;
			}
		}
		#endif

		return positionList;
	}

	/**************************************************************************************
    @brief  	タッチされた場所のデルタ座標を返す
    @param[in]	同時に何本指タッチまで許容するかの設定（スマートフォンのみに関係）
    @return 	Editor上は必ずサイズは１
    			スマートフォンのサイズはmaxTouchCountと同じ
    */
	public List<TouchPosition> GetdeltaPosition(int maxTouchCount)
	{

		List<TouchPosition> deltaPositionList = new List<TouchPosition> ();
		TouchPosition deltaPosition = new TouchPosition ();

		#if UNITY_EDITOR || UNITY_WINDOWS
		deltaPosition.X = m_mouseDiff.X - Input.mousePosition.x ;
		deltaPosition.Y = m_mouseDiff.Y - Input.mousePosition.y;
		m_mouseDiff.X = Input.mousePosition.x;
		m_mouseDiff.Y = Input.mousePosition.y;

		deltaPositionList.Add(deltaPosition);
		#elif UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0 && Input.touchCount <= maxTouchCount)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					deltaPosition.X = touch.deltaPosition.x;
					deltaPosition.Y = touch.deltaPosition.y;
					deltaPositionList.Add(deltaPosition);
				}
				break;
			}
		}
		#endif

		return deltaPositionList;
	}
}

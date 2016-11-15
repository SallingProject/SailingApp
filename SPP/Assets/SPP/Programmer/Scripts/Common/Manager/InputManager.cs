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

public class InputManager : BaseObjectSingleton<InputManager> {

    Vector2 m_mouseDiff = Vector2.zero;
	/**************************************************************************************
    @brief  	タッチされた場所の座標を返す
    @param[in]	同時に何本指タッチまで許容するかの設定（スマートフォンのみに関係）
    @return 	Editor上は必ずサイズは１
    			スマートフォンのサイズはmaxTouchCountと同じ
    */
	public List<Vector2> mGetPosition(int maxTouchCount)
	{

		List<Vector2> positionList = new List<Vector2> ();
        Vector2 position = new Vector2();

        #if UNITY_EDITOR || UNITY_WINDOWS
        position = Input.mousePosition;
		m_mouseDiff = position;
		positionList.Add(position);

		#elif UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0 && Input.touchCount <= maxTouchCount)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
                    position = -touch.position;
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
	public List<Vector2> mGetDeltaPosition(int maxTouchCount)
	{

		List<Vector2> deltaPositionList = new List<Vector2> ();
        Vector2 deltaPosition = new Vector2();

        #if UNITY_EDITOR || UNITY_WINDOWS
        deltaPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - m_mouseDiff;
		       
		m_mouseDiff = Input.mousePosition;
		
		deltaPositionList.Add(deltaPosition);
		#elif UNITY_ANDROID || UNITY_IOS

		if (Input.touchCount > 0 && Input.touchCount <= maxTouchCount)
		{
			foreach (Touch touch in Input.touches)
			{
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				{
					deltaPosition = -touch.deltaPosition;
					deltaPositionList.Add(deltaPosition);
				}
				break;
			}
		}
		#endif

		return deltaPositionList;
	}
}

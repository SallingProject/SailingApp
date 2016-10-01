﻿/**************************************************************************************/
/*! @file   ObjectUpdater.cs
***************************************************************************************
@brief      ゲームの更新処理クラス。ObjectBaseを継承したオブジェクトの更新処理用
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************/
using UnityEngine;
/**************************************************************************************
@brief  ゲームの更新処理クラス。
@note   Update関数を使うためMonoBehaviorを継承している　  
*/
public class BaseObjectUpdater : BaseObject {

    /**************************************************************************************
    @brief  更新処理。MonoBehaviorの実装。
    @note   このプロジェクトの唯一のUpdate関数。ここ以外にUpdateは存在するはずがない。
    */
    void Update()
    {

        foreach(var index in mObjectList)
        {
            index.mOnUpdate();
        }
        
    }

    /**************************************************************************************
    @brief  物理系の更新処理。MonoBehaviorの実装。
    @note   このプロジェクトの唯一のFixedUpdate関数。ここ以外にFixedUpdateは存在するはずがない。
    */
    void FixedUpdate()
    {
        foreach (var index in mObjectList)
        {
            index.mOnFixedUpdate();
        }
    }
}

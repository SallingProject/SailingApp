﻿/**********************************************************************************************/
/*! @file   GameObjectExtension.cs
*********************************************************************************************
@brief      GameObject型に対しての拡張メソッドをまとめたファイル
*********************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
**********************************************************************************************/
using UnityEngine;

/**************************************************************************************
@brief  GameObject型に対しての拡張メソッドをまとめた静的クラス
*/
public static class GameObjectExtension{

    /**************************************************************************************
    @brief        オブジェクトが存在するかの判定
    @return       存在する：true/存在しない：false
    */
    public static bool IsValid(this GameObject target)
    {
        return (target != null) ? true : false;

    }
}
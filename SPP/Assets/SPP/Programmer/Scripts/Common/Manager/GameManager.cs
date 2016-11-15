/**************************************************************************************/
/*! @file   GameManager.cs
***************************************************************************************
@brief      ゲーム全体のマネージャークラス
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;

public class GameManager : BaseObjectSingleton<GameManager> {

    /****************************************************************************** 
    @brief      初期化用
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

    }

    /****************************************************************************** 
    @brief      船のオブジェクト取得
    @return     none
    */
    public GameObject GetShipObject(EShipType type)
    {
        switch (type)
        {
            case EShipType.Class470:
                return null;

            case EShipType.Class49er:
                return null;

            case EShipType.ClassLaser:
                return null;

            case EShipType.ClassRS_X:
                return null;

            case EShipType.Invalid:
                return null;
        }
        return null;
    }
}

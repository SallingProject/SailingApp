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
    @brief      船のオブジェクトのパスを取得取得
    @return     none
    */
    public GameObject GetShipObject(EShipType type)
    {

        return Resources.Load(GetShipPath(type)) as GameObject;
    }

    /****************************************************************************** 
    @brief      船のオブジェクトのパスを取得取得
    @return     none
    */
    string GetShipPath(EShipType type)
    {
        switch (type)
        {
            case EShipType.Class470:
                return "Ship/Test";

            case EShipType.Class49er:
                return "Ship/Test";

            case EShipType.ClassLaser:
                return "Ship/Test";

            case EShipType.ClassRS_X:
                return "Ship/Test";

            case EShipType.Invalid:
                return "Ship/Test";
        }
        return null;
    }
}

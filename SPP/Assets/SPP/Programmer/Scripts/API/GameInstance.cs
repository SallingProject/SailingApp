/**************************************************************************************/
/*! @file   GameInstance.cs
***************************************************************************************
@brief      ゲームインスタンスクラス　シングルトン実装
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;

/******************************************************************************* 
@brief   ゲームインスタンスクラス　シングルトン実装
*/
public class GameInstance : BaseObjectSingleton<GameInstance> {

    /******************************************************************************
    @brief      更新処理管理クラスオブジェクト
    */
    [SerializeField]
    BaseObjectUpdater m_baseObjectUpdaterPref;
    BaseObjectUpdater m_baseObjectUpdater;


    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this); // mUpdateRunを呼び出す必要がないので管理から外す

        //初期化するべきオブジェクトの初期化や生成など

        // アップデータオブジェクトの生成
        if(m_baseObjectUpdaterPref != null)
        {
            m_baseObjectUpdater = mCreate(m_baseObjectUpdaterPref) as BaseObjectUpdater;
            m_baseObjectUpdater.transform.SetParent(this.transform);
        }
    }


}

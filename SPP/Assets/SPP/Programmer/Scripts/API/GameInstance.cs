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
using UnityEngine.SceneManagement;
/******************************************************************************* 
@brief   ゲームインスタンスクラス　シングルトン実装
*/
public class GameInstance : BaseObjectSingleton<GameInstance> {

    [SerializeField]
    BaseObjectUpdater m_updaterPrefbs;
    BaseObjectUpdater m_updater;


    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this); // mUpdateRunを呼び出す必要がないので管理から外す

        //初期化するべきオブジェクトの初期化や生成など
        if (m_updater != null)
        {
            m_updater = mCreate(m_updaterPrefbs) as BaseObjectUpdater;
        }
        this.transform.SetParent(m_updater.transform, false);
    }


}

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

    // BaseObjectのアップデーター
    [SerializeField]
    BaseObjectUpdater m_updaterPrefbs;
    BaseObjectUpdater m_updater;

    // スタティックキャンバス
    [SerializeField]
    GameObject m_staticCanvasPrefabs;
    GameObject m_staticCanvas;
    public GameObject mStaticCanvas
    {
        get { return m_staticCanvas; }
        private set { m_staticCanvas = value; }
    }

    [SerializeField]
    DebugManager m_debugManagerPref;
    DebugManager m_debugManager;

    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒。BaseObjectの実装
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this); // mUpdateRunを呼び出す必要がないので管理から外す

        //初期化するべきオブジェクトの初期化や生成など
        if (m_updater == null)
        {
            m_updater = mCreate(m_updaterPrefbs) as BaseObjectUpdater;
            m_updater.transform.SetParent(this.transform, false);
        }

        if (mStaticCanvas == null)
        {
            mStaticCanvas = mCreate(m_staticCanvasPrefabs) as GameObject;
            mStaticCanvas.name = "StaticCanvas";
        }

        if(m_debugManager == null)
        {
            m_debugManager = mCreate(m_debugManagerPref) as DebugManager;
            m_debugManager.transform.SetParent(this.transform, false);
        }
    }


}

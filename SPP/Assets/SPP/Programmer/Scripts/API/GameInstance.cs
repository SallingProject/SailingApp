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

    // シーンマネージャー
    [SerializeField]
    SceneManager m_sceneManagerPrefabs;
    SceneManager m_sceneManager;
    public SceneManager mSceneManager
    {
        get { return m_sceneManager; }
        private set { m_sceneManager = value; }
    }


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



    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒。BaseObjectの実装
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this); // mUpdateRunを呼び出す必要がないので管理から外す

        // ゲームに必要なプレハブとかを作成する。
        
        if(mSceneManager == null)
        {
            mSceneManager = mCreate(m_sceneManagerPrefabs) as SceneManager;
        }
        mSceneManager.transform.SetParent(this.transform, false);

        //初期化するべきオブジェクトの初期化や生成など
        if (m_updater == null)
        {
            m_updater = mCreate(m_updaterPrefbs) as BaseObjectUpdater;
        }
        m_updater.transform.SetParent(this.transform, false);

        if(mStaticCanvas == null)
        {
            mStaticCanvas = mCreate(m_staticCanvasPrefabs) as GameObject;
            mStaticCanvas.name = "StaticCanvas";
        }
    }


}

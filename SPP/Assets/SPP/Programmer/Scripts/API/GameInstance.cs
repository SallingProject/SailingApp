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
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
/******************************************************************************* 
@brief   ゲームインスタンスクラス　シングルトン実装
*/
public class GameInstance : BaseObjectSingleton<GameInstance> {

    const float kFadeAlphaValue = 1f;
    const float kCompleateLoad = 0.9f;

    public float mLoadProgress
    {
        get;
        private set;
    }

    // BaseObjectのアップデーター
    [SerializeField]
	private GameObject m_managerPrefbs;
	private GameObject m_manager;

    // スタティックキャンバス
    [SerializeField]
    private GameObject m_staticCanvasPrefabs;
    private GameObject m_staticCanvas;
    public GameObject mStaticCanvas
    {
        get { return m_staticCanvas; }
        private set { m_staticCanvas = value; }
    }
    [SerializeField]
    private DebugManager m_debugManagerPref;
    private DebugManager m_debugManager;

    private Image m_fade;

    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒。BaseObjectの実装
    @return     none
    */
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        //初期化するべきオブジェクトの初期化や生成など
		if (m_manager == null)
        {
			m_manager = mCreate(m_managerPrefbs) as GameObject;
			m_manager.transform.SetParent(this.transform, false);
        }

        if (mStaticCanvas == null)
        {
            mStaticCanvas = mCreate(m_staticCanvasPrefabs) as GameObject;
            mStaticCanvas.name = "StaticCanvas";
            mStaticCanvas.transform.SetParent(this.transform, false);
        }

        if(m_debugManager == null)
        {
            m_debugManager = mCreate(m_debugManagerPref) as DebugManager;
            m_debugManager.transform.SetParent(this.transform, false);
        }

        m_fade = m_staticCanvas.transform.FindChild("Fade").GetComponent<Image>();
    }

    /****************************************************************************** 
    @brief      非同期シーンロード
    @param[in]  次のシーンの名前
    @return     none
    */
    public void mAsyncLoad(string nextScene)
    {
        StartCoroutine(mOnAsyncLoad(nextScene));
    }

    /****************************************************************************** 
    @brief      同期シーンロード
    @param[in]  次のシーンの名前
    @return     none
    */
    public void mSyncLoad(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    /****************************************************************************** 
    @brief      非同期シーンの実処理
    @param[in]  次のシーンの名前
    @return     none
    */
    IEnumerator mOnAsyncLoad(string nextScene)
    {
        mLoadProgress = 0;
        m_fade.gameObject.SetActive(true);
        yield return null;
        // フェードイン
        while (m_fade.color.a < 1)
        {
            
            m_fade.color += new Color(0, 0, 0, kFadeAlphaValue * Time.deltaTime);
            yield return null;
        }
        
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
        async.allowSceneActivation = false;

        while (async.progress < kCompleateLoad)
        {
            mLoadProgress = async.progress;
            yield return new WaitForEndOfFrame();
        }
        
        async.allowSceneActivation = true;    // シーン遷移許可
        
        // フェードアウト
        while (m_fade.color.a > 0)
        {
            m_fade.color -= new Color(0, 0, 0, kFadeAlphaValue * Time.deltaTime);
            yield return null;
        }

        m_fade.gameObject.SetActive(false);
    }
}

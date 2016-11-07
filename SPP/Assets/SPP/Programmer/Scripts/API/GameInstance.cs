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

    // BaseObjectのアップデーター
    [SerializeField]
    private BaseObjectUpdater m_updaterPrefbs;
    private BaseObjectUpdater m_updater;

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
        if (m_updater == null)
        {
            m_updater = mCreate(m_updaterPrefbs) as BaseObjectUpdater;
            m_updater.transform.SetParent(this.transform, false);
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

    public void ChnageScene(string nextScene)
    {
        StartCoroutine(OnChangeScene(nextScene));
    }

    IEnumerator OnChangeScene(string nextScene)
    {

        m_fade.gameObject.SetActive(true);
        yield return null;
        while (m_fade.color.a < 1)
        {
            
            m_fade.color += new Color(0, 0, 0, 1 * Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(nextScene);

        while (m_fade.color.a > 0)
        {
            m_fade.color -= new Color(0, 0, 0, 1*Time.deltaTime);
            yield return null;
        }

        m_fade.gameObject.SetActive(false);
    }
}

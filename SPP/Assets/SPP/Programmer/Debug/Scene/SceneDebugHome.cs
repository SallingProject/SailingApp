/**************************************************************************************/
/*! @file   SceneDebugHome.cs
***************************************************************************************
@brief      デバッグシーンクラス
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class SceneDebugHome : SceneBase {

    [SerializeField]
    private List<GameObject> m_pageList;
    
    [SerializeField]
    private Button m_prev;

    [SerializeField]
    private Button m_next;

    private int m_pageCount;

    /****************************************************************************** 
    @brief      初期化用。タイミングはAwakeと一緒。BaseObjectの実装
    @return     none
    */
    protected override void Start()
    {
        base.Start();
        m_pageCount = 0;
        m_next.onClick.AddListener(() =>
        {
            m_pageList[m_pageCount].SetActive(false);
            m_pageCount += 1;
            if (m_pageCount >= m_pageList.Count)
            {
                m_pageCount = m_pageList.Count - 1;
            }
            m_pageList[m_pageCount].SetActive(true);

        });

        m_prev.onClick.AddListener(() =>
        {
            m_pageList[m_pageCount].SetActive(false);
            m_pageCount -= 1;
            if(m_pageCount < 0)
            {
                m_pageCount = 0;
            }
            m_pageList[m_pageCount].SetActive(true);
        });
    }

    /****************************************************************************** 
    @brief      タイトルへ飛ぶ
    @return     none
    */
    public void mGoToTitle()
    {
        GameInstance.mInstance.mAsyncLoad("Title");
    }

    /****************************************************************************** 
    @brief      ホームへ飛ぶ
    @return     none
    */
    public void mGoToHome()
    {
        GameInstance.mInstance.mAsyncLoad("Home");
    }

    /****************************************************************************** 
    @brief      ライブラリへ飛ぶ
    @return     none
    */
    public void mGoToLibrary()
    {
        GameInstance.mInstance.mAsyncLoad("Library");
    }
}

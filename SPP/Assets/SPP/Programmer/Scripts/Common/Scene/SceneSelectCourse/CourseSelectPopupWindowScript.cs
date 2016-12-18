/**************************************************************************************/
/*! @file   CourseSelectPopupWindowScript.cs
***************************************************************************************
@brief      コースセレクト時のポップアップに関する処理
***************************************************************************************
@author     Ryo Sugiyama
***************************************************************************************
* Copyright © 2016 RyoSugiyama All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CourseSelectPopupWindowScript : PopupBase
{

    [SerializeField]
    private GameObject m_contens;
    [SerializeField]
    private ECourseType m_type;

    public void OpenEnd()
    {
        m_contens.SetActive(true);
    }
    public void CloseEnd()
    {
        m_contens.SetActive(false);
    }
    void PopupAction(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                PlayerPrefs.SetInt(SaveKey.mTutorialKey, (int)m_type);
                GameInstance.mInstance.mSceneLoad(new LoadInfo("Tutorial"));
                Close();
                break;
            case EButtonId.Cancel:
                Close();
                break;
        }
    }
    public void GetCourseType(ECourseType type)
    {
        m_type = type;
    }
    public void Open()
    {

        mButtonSet = EButtonSet.Set2;
        PopupButton.mOnClickCallback = PopupAction;
        base.Open(null, null, OpenEnd);

    }

    public void Close()
    {
        base.Close(CloseEnd, null, null);
    }

}

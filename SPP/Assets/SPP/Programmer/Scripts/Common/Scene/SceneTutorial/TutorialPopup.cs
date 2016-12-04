/**************************************************************************************/
/*! @file   PopupWindow.cs
***************************************************************************************
@brief     チュートリアル時のポップアップに関する処理
***************************************************************************************
@author     Ryo Sugiyama
***************************************************************************************
* Copyright © 2016 RyoSugiyama All Rights Reserved.
***************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialPopup : PopupBase
{

    [SerializeField]
    private ECourseType m_Type;
    [SerializeField]
    private GameObject m_comtens;

    private System.Action m_endCallback;


	public void StartPopup ()
    {
        m_comtens.SetActive(true);
	}

    public void EndPopup()
    {
        m_comtens.SetActive(false);
    }

    void SkipPopupActuon()
    {
        base.Close(null, null, EndPopup);

        Debug.Log("Cancel");

        if (m_endCallback != null)
            m_endCallback.Invoke();
    }

    void PopupActionFourTimes(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                SkipPopupActuon();
                break;
        }
    }

    void PopupActionThreeTimes(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                mButtonSet = EButtonSet.Set2;
                PopupButton.mOnClickCallback = PopupActionFourTimes;
                break;
            case EButtonId.Cancel:
                SkipPopupActuon();

                break;
        }
    }

	void PopupActionTwice(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                mButtonSet = EButtonSet.Set2;
                PopupButton.mOnClickCallback = PopupActionThreeTimes;
                break;
            case EButtonId.Cancel:
                SkipPopupActuon();

                break;
        }
    }

	void PopupActionOnce (EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                mButtonSet = EButtonSet.Set2;
                PopupButton.mOnClickCallback = PopupActionTwice;
                break;
            case EButtonId.Cancel:
                SkipPopupActuon();

                break;
        }
	}

   public void GetCoueseType(ECourseType type)
    {
        m_Type = type;
    }

   public void Open(System.Action openedCallback,System.Action endCallback)
    {
        GetCoueseType(m_Type);
        mButtonSet = EButtonSet.Set2;
        PopupButton.mOnClickCallback = PopupActionOnce;
        base.Open(null, null,
            () =>
            {
                StartPopup();
                openedCallback.Invoke();
            });

        m_endCallback = endCallback;
    }
}

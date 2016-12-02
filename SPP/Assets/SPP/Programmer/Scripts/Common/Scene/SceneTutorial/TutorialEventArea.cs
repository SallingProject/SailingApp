/**************************************************************************************/
/*! @file   TutorialEventArea.cs
***************************************************************************************
@brief      チュートリアルのイベント発生時におこるやつ
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;

public class TutorialEventArea : BaseObject
{
    [SerializeField]
    int m_eventId = 0;

    [SerializeField]
    bool m_isOneTimeOnly = false;

    bool m_used = false;

    public System.Action<int> mEventCallback
    {
        private get;
        set;
    }

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this);
    }

    void OnTriggerEnter(Collider other)
    {

        if(mEventCallback != null && !m_used)
        {
            mEventCallback.Invoke(m_eventId);

            m_used = m_isOneTimeOnly ? true : false;
        }
    }

    /**************************************************************************************
    @brief  	イベント開始時に呼ぶ
    */
    public void BeginEvent()
    {
        Time.timeScale = 0;
    }

    /**************************************************************************************
    @brief  	イベント終了時に呼ぶ
    */
    public void ExitEvent()
    {
        Time.timeScale = 1;
    }
}

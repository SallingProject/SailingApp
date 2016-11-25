/**************************************************************************************/
/*! @file   PopupBase.cs
***************************************************************************************
@brief      PopupWindowの基底クラス
***************************************************************************************
@author     Ko Hashimoto and Kana Yoshidumi
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PopupBase : BaseObject {


    [SerializeField]
    RectTransform m_popupWindowBase;
    RectTransform m_popupWindow;

    [SerializeField]
    RectTransform m_popupRoot;
    
    PopupButton m_popupButton;
    

    class PopupAction
    {
        public System.Action _begin;
        public System.Action _run;
        public System.Action _end;
    }

    PopupAction m_openAction = new PopupAction();
    PopupAction m_closeAction = new PopupAction();

    public EButtonSet mButtonSet
    {
        private get;
        set;
    }

    float m_time = float.NaN;
    protected override void Awake()
    {
        base.Awake();
        mUnregisterList(this);
        mUnregister();
    }

    public EPopupState mPopupState
    {
        get;
        private set;
    }

    public virtual void Open(System.Action openBeginAction, System.Action openning = null, System.Action openEnd = null, float time = 0.25f)
    {
        if(m_popupWindow == null)
        {
            m_popupWindow = mCreate(m_popupWindowBase) as RectTransform;
        }
        m_popupWindow.SetParent(m_popupRoot, false);
        m_popupWindow.transform.localScale = new Vector3(1, 0, 0);

        if (m_popupButton == null)
        {
            var parent = transform.FindInChildren("PopupBase_M(Clone)", false);
            var buttonGroup = parent.transform.FindInChildren("Button", false);

            string path = (mButtonSet == EButtonSet.Set1) ? "ButtonSet1" : "ButtonSet2";
            var button = buttonGroup.transform.FindInChildren(path, false);
            m_popupButton = button.GetComponent<PopupButton>();
            m_popupButton.Init();
        }

        m_openAction._begin = openBeginAction;
        m_openAction._run = openning;
        m_openAction._end = openEnd;
        m_time = time;

        OnOpen();
    }

    public virtual void Close(System.Action closeBeginAction, System.Action closening = null, System.Action closeEnd = null, float time = 0.25f)
    {
        m_closeAction._begin = closeBeginAction;
        m_closeAction._run = closening;
        m_closeAction._end = closeEnd;
        m_time = time;

        OnCloseAnimation();
    }

    void OnOpen()
    {
        var tweener = m_popupWindow.DOScale(new Vector3(1f, 1f), m_time).SetEase(Ease.InOutQuart);
        tweener
        .OnStart(()=>
        {
            if (m_openAction._begin != null)
            {
                m_openAction._begin.Invoke();
                m_openAction._begin = null;
            }
            mPopupState = EPopupState.OpenBegin;
        })
        .OnUpdate(() =>
        {
            if (m_closeAction._run != null)
            {
                m_openAction._run.Invoke();
                m_openAction._run = null;
            }
            mPopupState = EPopupState.Openning;
        })
         .OnComplete(() =>
         {

             if (m_closeAction._end != null)
             {
                 m_openAction._end.Invoke();
                 m_openAction._end = null;
             }
             mPopupState = EPopupState.OpenEnd;
             m_popupButton.transform.SetActive(true);
         });
        
    }

    void OnCloseAnimation()
    {
        var tweener = m_popupWindow.DOScale(new Vector3(1f, 0f), m_time).SetEase(Ease.InOutQuart);
        tweener.OnStart(()=>
        {
            if (m_closeAction._begin != null)
            {
                m_closeAction._begin.Invoke();
                m_closeAction._begin = null;
            }
            m_popupButton.transform.SetActive(false);
            mPopupState = EPopupState.CloseBegin;
        })
         .OnUpdate(() =>
         {
             if (m_closeAction._run != null)
             {
                 m_closeAction._run.Invoke();
                 m_closeAction._run = null;
             }
             mPopupState = EPopupState.Closing;
         })
         .OnComplete(() =>
         {

             if (m_closeAction._end != null)
             {
                 m_closeAction._end.Invoke();
                 m_closeAction._end = null;
             }
             mPopupState = EPopupState.CloseEnd;
         });
    }


}

using UnityEngine;
using System.Collections.Generic;

public class TutorialEventManager : BaseObject {

    [SerializeField]
    TutorialPopup m_popup;
    
    [SerializeField]
    List<TutorialEventArea> m_eventAreaList = new List<TutorialEventArea>();

    [SerializeField]
    RectTransform m_contentRoot;

    GameObject m_visibleContent;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this);

        foreach(var index in m_eventAreaList)
        {
            SetEventArea(index);   
        }
    }


    /**************************************************************************************
    @brief  	イベント設定用。ラムダ式での設定のため重複回避のため関数わけ
    */
    void SetEventArea(TutorialEventArea area)
    {
        area.mEventCallback = id =>
        {
            m_popup.mButtonCallback = buttonType =>
            {

                m_contentRoot.SetActive(false);
                mDelete(m_visibleContent);

                switch (buttonType)
                {
                    case EButtonId.Ok:
                        area.mAnimationId += 1;
                        if (area.mAnimationId >= area.Animations.Count - 1)
                        {
                            m_popup.mButtonSet = EButtonSet.Set1;
                            m_popup.mButtonCallback = type => { m_popup.Close(() => { area.ExitEvent(); m_contentRoot.SetActive(false); });};
                        }

                        var next = mCreate(area.Animations[area.mAnimationId]);
                        next.transform.SetParent(m_contentRoot.transform, false);

                        m_visibleContent = next;
                        
                        break;

                    case EButtonId.Cancel:
                        m_popup.mButtonCallback = type => { m_popup.Close(() => { area.ExitEvent(); m_contentRoot.SetActive(false); }); };
                        break;
                }

                m_contentRoot.SetActive(true);
            };

            area.mAnimationId = 0;
            var first = mCreate(area.Animations[area.mAnimationId]);
            first.transform.SetParent(m_contentRoot.transform, false);
            m_visibleContent = first;
            m_popup.mButtonSet = EButtonSet.Set2;
            m_popup.Open(m_contentRoot.gameObject, area.BeginEvent);
        };
    }
}

using UnityEngine;
using System.Collections.Generic;

public class TutorialEventManager : BaseObject {

    [SerializeField]
    TutorialPopup m_popup;

    [SerializeField]
    List<TutorialEventArea> m_eventAreaList = new List<TutorialEventArea>();

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        mUnregisterList(this);

        foreach(var index in m_eventAreaList)
        {
            index.mEventCallback = id =>
            {
                m_popup.Open(index.BeginEvent, index.ExitEvent);
            };
        }
    }
}

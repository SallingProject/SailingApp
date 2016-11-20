using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using System.Collections;

public class UIItem : BaseObject {

    [SerializeField]
    private Image m_image;

    ITouchInfo m_touch;
    public bool mIsDown
    {
        get;
        private set;
    }
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        mUnregisterList(this);

        EventTrigger trigger = m_image.GetComponent<EventTrigger>();

        // PointerDownイベントの追加
        {
            EventTrigger.Entry down = new EventTrigger.Entry();
            down.eventID = EventTriggerType.PointerDown;
            down.callback.RemoveAllListeners();
            down.callback.AddListener(Down);
            trigger.triggers.Add(down);
        }

        // PointerUpイベントの追加
        {
            EventTrigger.Entry up = new EventTrigger.Entry();
            up.eventID = EventTriggerType.PointerUp;
            up.callback.RemoveAllListeners();
            up.callback.AddListener(Up);
            trigger.triggers.Add(up);
        }

        // Dragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.RemoveAllListeners();
            drag.callback.AddListener(Drag);
            trigger.triggers.Add(drag);
        }
    }

    void Down(BaseEventData eventData)
    {

        mIsDown = true;
        m_touch = InputManager.mInstance.mGetTouchInfo();

    }

    void Drag(BaseEventData eventData)
    {
    }
    
    void Up(BaseEventData eventData)
    {
        StartCoroutine(ItemUp());
    }
    
    // フリックが１フレーム遅れててさｗ
    IEnumerator ItemUp()
    {
        yield return null;

        var touch = InputManager.mInstance.mGetTouchInfo(m_touch.mFingerId);
        DebugManager.mInstance.OutputMsg(touch.mTouchType, ELogCategory.Default, true);
        if (touch.mTouchType == ETouchType.Flick)
        {
            DebugManager.mInstance.OutputMsg("アイテム発動", ELogCategory.Default, true);
        }

        mIsDown = false;
    }

    public void SetItem(ItemDefine item)
    {

    }
}

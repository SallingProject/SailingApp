using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SceneLibrary : BaseObject {

    [SerializeField]
    Image m_touchPanel;

    [SerializeField]
    GameObject m_shipRoot;

    protected override void mOnRegistered()
    {
        mUnregisterList(this);


        EventTrigger trigger = m_touchPanel.GetComponent<EventTrigger>();

        // Dragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.AddListener(eventData =>
            {
                var rotation = InputManager.mInstance.mGetDeltaPosition(1);
                m_shipRoot.transform.Rotate(new Vector3(m_shipRoot.transform.rotation.y + rotation[0].Y, m_shipRoot.transform.rotation.x + rotation[0].X, 0));
            });

            trigger.triggers.Add(drag);
        }
    }
}

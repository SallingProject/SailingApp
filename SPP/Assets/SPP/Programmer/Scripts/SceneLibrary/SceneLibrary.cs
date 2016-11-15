using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SceneLibrary : SceneBase {

    [SerializeField]
    Image m_touchPanel;

    [SerializeField]
    GameObject m_shipRoot;

    [SerializeField]
    Button m_resetButton;

    [SerializeField]
    Camera m_camera;
    protected override void mOnRegistered()
    {
        base.mOnRegistered();

        EventTrigger trigger = m_touchPanel.GetComponent<EventTrigger>();

        // PointerDragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.AddListener(eventData =>
            {
                var rotationList = InputManager.mInstance.mGetDeltaPosition(1);

                Vector3 rotation = m_shipRoot.transform.localEulerAngles;

                m_shipRoot.transform.Rotate(new Vector3(rotationList[0].y, rotationList[0].x, m_shipRoot.transform.rotation.z));

            });

            trigger.triggers.Add(drag);
        }

        // PointerDownイベントの追加
        //{
        //    EventTrigger.Entry down = new EventTrigger.Entry();
        //    down.eventID = EventTriggerType.PointerDown;
        //    down.callback.AddListener(eventData =>
        //    {
        //        var rotationList = InputManager.mInstance.mGetPosition(1);
        //        Vector3 rotation = m_shipRoot.transform.localEulerAngles;

        //        m_shipRoot.transform.localEulerAngles = new Vector3(rotationList[0].y, rotationList[0].x, m_shipRoot.transform.rotation.z);
        //    });

        //    trigger.triggers.Add(down);
        //}

        
        // リセット処理
        m_resetButton.onClick.AddListener(() =>
        {
            m_shipRoot.transform.localEulerAngles = new Vector3(0, -150, 0);
        });
    }

    
}

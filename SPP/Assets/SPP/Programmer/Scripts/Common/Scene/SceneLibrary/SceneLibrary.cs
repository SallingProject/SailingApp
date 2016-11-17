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
                var touch = InputManager.mInstance.mGetTouchInfo(0);
                Vector3 rotation = m_shipRoot.transform.localEulerAngles;
                
                m_shipRoot.transform.Rotate(new Vector3(touch._deltaPosition.y, touch._deltaPosition.x, m_shipRoot.transform.rotation.z));

            });

            trigger.triggers.Add(drag);
        }
        
        // リセット処理
        m_resetButton.onClick.AddListener(() =>
        {
            m_shipRoot.transform.localEulerAngles = new Vector3(0, -150, 0);
        });
    }
}

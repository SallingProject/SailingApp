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

    [SerializeField]
    Vector3 m_initRotation = new Vector3(0, -150, 0);
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

                
                //移動量に応じて角度計算
                float xAngle = touch._deltaPosition.y * touch._speed;
                float yAngle = -touch._deltaPosition.x * touch._speed;
                float zAngle = 0;

                //回転
                m_shipRoot.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
            });

            trigger.triggers.Add(drag);
        }
        
        // リセット処理
        m_resetButton.onClick.AddListener(() =>
        {
            m_shipRoot.transform.localEulerAngles = m_initRotation;
        });
    }
}

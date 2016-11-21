using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SceneLibrary : SceneBase {

    [SerializeField]
    private Image m_touchPanel;

    [SerializeField]
    private GameObject m_shipRoot;

    [SerializeField]
    private Button m_resetButton;

    [SerializeField]
    private Camera m_camera;

    [SerializeField]
    private Vector3 m_initRotation = new Vector3(0, -150, 0);
    private Vector3 m_initCameraPosition = Vector3.zero;

    private float m_prevDistance = 0;
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        
        EventTrigger trigger = m_touchPanel.GetComponent<EventTrigger>();

        // PointerDragイベントの追加
        {
            EventTrigger.Entry drag = new EventTrigger.Entry();
            drag.eventID = EventTriggerType.Drag;
            drag.callback.RemoveAllListeners();
            drag.callback.AddListener(Drag);

            trigger.triggers.Add(drag);
        }
        
        // リセット処理
        m_resetButton.onClick.AddListener(() =>
        {
            m_camera.transform.position = m_initCameraPosition;
            m_shipRoot.transform.localEulerAngles = m_initRotation;
        });

        m_initCameraPosition = m_camera.transform.position;
    }

    void Drag(BaseEventData eventData)
    {
        if (InputManager.mInstance.mTouchCount < 2)
        {


            var touch = InputManager.mInstance.mGetTouchInfo();
            //移動量に応じて角度計算
            float xAngle = touch.mDeltaPosition.y;
            float yAngle = -touch.mDeltaPosition.x;

            if (Mathf.Abs(xAngle) > Mathf.Abs(yAngle))
            {
                m_shipRoot.transform.Rotate(Vector3.right * xAngle / 2, Space.World);
            }
            else
            {
                m_shipRoot.transform.Rotate(Vector3.up * yAngle / 2, Space.World);
            }
        }
        else
        {
            var touch1 = InputManager.mInstance.mGetTouchInfo(0);
            var touch2 = InputManager.mInstance.mGetTouchInfo(1);


            float distance = Vector2.Distance(touch1.mPosition, touch2.mPosition);

            if(m_prevDistance > distance)
            {
                // 近づける処理
                m_camera.transform.position += new Vector3(0, 0, 1);
            }
            else
            {
                //　遠ざける処理

                m_camera.transform.position -= new Vector3(0, 0, 1);

            }
            m_prevDistance = distance;
        }
    }
}

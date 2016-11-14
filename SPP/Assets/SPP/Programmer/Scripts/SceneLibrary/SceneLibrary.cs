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
        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener(eventData =>
        {
            var rotationList = InputManager.mInstance.mGetDeltaPosition(1);
            
            m_shipRoot.transform.Rotate(new Vector3(rotationList[0].y, rotationList[0].x, 0));
        });

        trigger.triggers.Add(drag);

        // リセット処理
        m_resetButton.onClick.AddListener(() =>
        {
            m_shipRoot.transform.rotation = new Quaternion();
        });
    }

    
}

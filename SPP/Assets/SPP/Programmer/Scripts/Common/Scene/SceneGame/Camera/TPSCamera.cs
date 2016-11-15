using UnityEngine;
using System.Collections;

public class TPSCamera : BaseObject{
    [SerializeField]
    private Vector3 m_baseOffsetTPS;

    [SerializeField]
    private Vector3 m_baseRotationTPS;

    protected override void Start()
    {
    }

    public override void mOnUpdate()
    {
        bool isTrigger = GameInfo.mInstance.mControllerTrigger;
        if (isTrigger) OnController();
        else UnController();
    }

    private void OnController()
    {
        float rota = GameInfo.mInstance.mGetHandleRotation();
        transform.localRotation = Quaternion.Euler(m_baseRotationTPS.x, rota, m_baseRotationTPS.z);
        Matrix4x4 mat = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(rota, Vector3.up), Vector3.one);
        transform.localPosition = mat * m_baseOffsetTPS;
    }

    private void UnController()
    {
        //回転中なら逆方向に補正する
        float vecRote = GetComponentInParent<ShipMove>().mRotateValue;
        if (vecRote != 0)
        {
            transform.localEulerAngles -= new Vector3(0, vecRote, 0);
            Matrix4x4 mat = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.up), Vector3.one);
            transform.localPosition = mat * m_baseOffsetTPS;
        }
        else
        {
            //transform.localPosition = m_baseOffsetTPS;
            //transform.localEulerAngles = m_baseRotationTPS;
        }
    }



}

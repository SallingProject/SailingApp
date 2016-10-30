/**************************************************************************************/
/*! @file   Point.cs
***************************************************************************************
@brief      ポイントの判定
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/
using UnityEngine;
using System.Collections;

public class Point: PointArrayObject{

    [SerializeField]
    [Range(-180,180)]
    private float m_inAngle;     //始まりの角度

    [SerializeField]
    [Range(-180, 180)]
    private float m_outAngle;       //終わりの角度

    [SerializeField]
    private float m_radius;         //サークル半径

    [SerializeField]
    private GameObject m_detectionPrefab;

    private GameObject m_inAngleObject;
    private GameObject m_outAngleObject;

    private bool m_stayArea;    //エリア内フラグ

    private const float mk_scaleY = 0.01f;


    protected override void Start()
    {
        {   //入りのオブジェクトの生成
            var Obj = BaseObject.mCreate(m_detectionPrefab);
            Obj.transform.parent = transform;
            Obj.transform.localScale = new Vector3(1, mk_scaleY, m_radius);
            Obj.transform.Rotate(0, m_inAngle, 0);
            Obj.transform.position = transform.position;
            Obj.transform.Translate(0, 0, m_radius + 3);
            Obj.transform.name = "Bigin";
            m_inAngleObject = Obj;
        }
        {   //終わりのオブジェクトの生成
            var Obj= BaseObject.mCreate(m_detectionPrefab);
            Obj.transform.parent = transform;
            Obj.transform.localScale = new Vector3(1, mk_scaleY, m_radius);
            Obj.transform.Rotate(0, m_outAngle, 0);
            Obj.transform.position = transform.position;
            Obj.transform.Translate(0, 0, m_radius + 3);
            Obj.transform.name = "End";
            m_outAngleObject = Obj;
        }
        transform.GetComponent<SphereCollider>().radius = m_radius;

    }

    override public void mOnUpdate()
    {
        if (!enabled) return;       //そもそもスクリプトがONじゃない場合<<Return
        //エリア外なら元に戻す
        if (!m_stayArea)
        {
            m_inAngleObject.GetComponent<CollisionDetection>().mIsEntered = false;
            m_outAngleObject.GetComponent<CollisionDetection>().mIsEntered = false;
            return;
        }
        //すべて通っていたら判定
        if (m_inAngleObject.GetComponent<CollisionDetection>().mIsEntered)
        {
            if (m_outAngleObject.GetComponent<CollisionDetection>().mIsEntered)
            {
                mNext();
            }
        } else if (m_outAngleObject.GetComponent<CollisionDetection>().mIsEntered){
            m_outAngleObject.GetComponent<CollisionDetection>().mIsEntered = false;
        }

    }


    //ポイント周囲の空間にはいっているか
    void OnTriggerEnter(Collider col)
    {
        m_stayArea = true;
    }
    void OnTriggerExit(Collider col)
    {
        m_stayArea = false;
    }

    private void OnDisable()
    {
        m_inAngleObject.SetActive(false);
        m_outAngleObject.SetActive(false);
    }

}

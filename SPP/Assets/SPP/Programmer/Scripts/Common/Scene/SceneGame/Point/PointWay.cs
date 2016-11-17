using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointWay : BaseObject
{

    [SerializeField]
    private GameObject m_wayPrefab;

    private Vector3 m_point;
    private float angle;

    float m_now;
    float m_prevpos;
    float m_distance;


//    public Text m_scoreText;

    [SerializeField]
    private GameObject m_ship;

    [SerializeField]
    private BaseObject m_firstPoint;
    [SerializeField]
    private BaseObject m_secondPoint;

    private PointArrayObject m_pointArray;

    public override void mOnUpdate()
    {
        m_secondPoint = m_pointArray.mGetPoint();
        m_firstPoint = m_pointArray.mGetPrevPoint();
        if (!m_firstPoint.IsValid()) return;
        if (!m_secondPoint.IsValid()) return;
        

        m_point = m_secondPoint.transform.position + m_firstPoint.transform.position;

        m_wayPrefab.transform.position = m_point / 2;

        m_point = m_secondPoint.transform.position - m_wayPrefab.transform.position;
        m_wayPrefab.transform.localScale = new Vector3(m_point.magnitude*2,0.01f,1.0f);

        angle = Mathf.Atan2(m_point.z, m_point.x);

        angle *= Mathf.Rad2Deg;
        m_wayPrefab.transform.rotation = Quaternion.AngleAxis(-angle, new Vector3(0, 1, 0));

        mOnPointWay();

    }

    // Use this for initialization
    override protected void Start()
    {
        m_pointArray = GameInfo.mInstance.m_pointArray;

        m_wayPrefab = mCreate(m_wayPrefab);

//        m_scoreText.text = m_distance.ToString();

    }

    void mOnPointWay()
    {
        m_now = Vector3.Distance(m_secondPoint.transform.position, m_ship.transform.position);

        //Debug.Log("m_now:" + m_now);

        if(m_prevpos == 0)
        {
            m_prevpos = m_now;
        }

        m_distance += -(m_now - m_prevpos);

//        m_scoreText.text = m_distance.ToString();

//        Debug.Log("m_distance" + m_distance);

       // Debug.Log("m_prev:"+m_prevpos);

        m_prevpos = m_now;

    }
}

using UnityEngine;
using System.Collections;

public class PointWay : BaseObject
{

    public int Max_point;                // 作成するPointの最大数.
    private GameObject[] Pointprefab;

    private Vector3 m_point;
    private float angle;

    public override void mOnUpdate()
    {

        if(Input.GetKey(KeyCode.LeftArrow)){
            Pointprefab[0].transform.position += new Vector3(-1.0f,0f,0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Pointprefab[0].transform.position += new Vector3(1.0f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Pointprefab[0].transform.position += new Vector3(0.0f, 0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Pointprefab[0].transform.position += new Vector3(0.0f, 0f, -1.0f);
        }


        m_point = Pointprefab[0].transform.position + Pointprefab[1].transform.position;

        Pointprefab[2].transform.position = m_point / 2;

        m_point = Pointprefab[1].transform.position - Pointprefab[2].transform.position;
        Pointprefab[2].transform.localScale = new Vector3(m_point.magnitude*2,0.01f,1.0f);

        angle = Mathf.Atan2(m_point.z, m_point.x);
        Debug.Log(m_point);

        angle *= Mathf.Rad2Deg;
 
        Pointprefab[2].transform.rotation = Quaternion.AngleAxis(-angle, new Vector3(0, 1, 0));

    }

    // Use this for initialization
    override protected void Start()
    {
        if (Max_point == 0) return;

        Pointprefab = new GameObject[Max_point];

        for (int i = 0; i < Max_point; i++)
        {
            // GameObjectの作成.
            Pointprefab[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        Pointprefab[0].transform.localPosition = new Vector3(1.24f, 0.02f, -4.0f);
        Pointprefab[1].transform.localPosition = new Vector3(-5.41f, 0.02f, 6.57f);

    }

}

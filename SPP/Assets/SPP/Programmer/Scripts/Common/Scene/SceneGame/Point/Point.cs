/**************************************************************************************/
/*! @file   Point.cs
***************************************************************************************
@brief      ポイントの判定
***************************************************************************************
@note       生成された時管理配列に登録される。
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;

public class Point: PointArrayObject{
    [System.Serializable]
    class BuoyDetermination
    {
        public enum eDirection
        {
            Back = -1, NotUse = 0 ,Forward = 1,
        }

        [Range(-180,180)]
        public float m_angle;     //始まりの角度
        public eDirection m_direction = eDirection.NotUse;     //方向
        public string m_name = "";
    }

    [SerializeField]
    private BuoyDetermination[] m_point;

    [SerializeField]
    private float m_radius;         //サークル半径

    [SerializeField]
    private GameObject m_detectionPrefab;   //当たり判定用プレハブ

    private GameObject[] m_angleObject;

    private bool m_stayArea;    //エリア内フラグ
    private int m_index;

    private const float mk_scaleY = 0.01f;  //縦固定値


    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        {   //入りのオブジェクトの生成
            m_angleObject = new GameObject[m_point.Length];
            for(int i=0; i<m_point.Length; ++i)
            {
                if (m_point[i].m_direction != 0)
                {
                    m_angleObject[i] = mCollisionCreate(m_point[i]);
                }
                i++;
            }
        }
        m_index = 0;
        transform.GetComponent<SphereCollider>().radius = m_radius;

    }
    /****************************************************************************** 
    @brief      ポイント判定用板の生成 （簡略化用）
    @in         インスペクターから受け取る配置角度、向きなど
    @return     生成されたオブジェクト
    */
    GameObject mCollisionCreate(BuoyDetermination buoy)
    {
        var Obj = BaseObject.mCreate(m_detectionPrefab);
        Obj.transform.parent = transform;
        Obj.transform.localScale = new Vector3(1, mk_scaleY, m_radius);
        Obj.transform.Rotate(0, buoy.m_angle, 0);
        Obj.transform.position = transform.position;
        Obj.transform.Translate(0, 0, m_radius + 3);
        Obj.transform.name = buoy.m_name;
        Obj.GetComponent<CollisionDetection>().mDirection = (int)buoy.m_direction;
        return Obj;

    }

    override public void mOnUpdate()
    {
        if (!enabled) return;       //そもそもスクリプトがONじゃない場合<<Return
//        Debug.Log("come:"+name);
        //エリア外なら元に戻す
        if (!m_stayArea)
        {
            foreach(var obj in m_angleObject)
            {
                obj.GetComponent<CollisionDetection>().mIsEntered = false;
            }
            return;
        }
        //すべて通っていたら次へ
        if (m_angleObject[m_index].GetComponent<CollisionDetection>().mIsEntered)
        {
            if(m_angleObject.Length-1 == m_index)
            {
                mNext();
                return; ;
            }
            m_index++;
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

    private void OnEnable()
    {
        foreach (var obj in m_angleObject)
        {
            obj.SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (var obj in m_angleObject)
        {
            obj.SetActive(false);
        }
    }

}

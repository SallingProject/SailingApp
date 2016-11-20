﻿/**************************************************************************************/
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


public class Point : BaseObject{
    [System.Serializable]
    public class BuoyDetermination
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
    private BuoyDetermination[] m_determination;

    public int[] m_pointId;

    [SerializeField]
    private float m_radius;         //サークル半径

    [SerializeField]
    private GameObject m_detectionPrefab;   //当たり判定用プレハブ

    private GameObject[] m_angleObject;

    private bool m_stayArea;    //エリア内フラグ
    private int m_index;

    private const float mk_scaleY = 0.01f;  //縦固定値

    private PointArrayObject m_pointArray;
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
        //管理配列に登録
        m_pointArray = GameInfo.mInstance.m_pointArray;
        {   //入りのオブジェクトの生成
            m_angleObject = new GameObject[m_determination.Length];
            for(int i=0; i< m_determination.Length; i++)
            {
                if (m_determination[i].m_direction != 0)
                {
                    mCollisionCreate(m_determination[i], out m_angleObject[i]);
                }
            }
        }
        m_index = 0;
        transform.GetComponent<SphereCollider>().radius = m_radius;

        foreach (var i in m_pointId){
            m_pointArray.mRegisterArray(i, this);
        }
        enabled = false;
    }
    /****************************************************************************** 
    @brief      ポイント判定用板の生成 （簡略化用）
    @in         インスペクターから受け取る配置角度、向きなど
    @return     生成されたオブジェクト
    */
    private void mCollisionCreate(BuoyDetermination buoy,out GameObject receive)
    {
        var Obj = mCreate(m_detectionPrefab);
        receive = Obj;
        receive.transform.parent = transform;
        receive.transform.localScale = new Vector3(1, mk_scaleY, m_radius);
        receive.transform.Rotate(0, buoy.m_angle, 0);
        receive.transform.localPosition = Vector3.zero;
        receive.transform.Translate(0, 0, m_radius + 3);
        receive.transform.name = buoy.m_name;
        receive.GetComponent<CollisionDetection>().mDirection = (int)buoy.m_direction;

    }

    override public void mOnUpdate()
    {
        if (!enabled) return;       //そもそもスクリプトがONじゃない場合<<Return
        //エリア外なら元に戻す
        if (!m_stayArea)
        {
            foreach(var obj in m_angleObject)
            {
                obj.GetComponent<CollisionDetection>().mIsEntered = false;
            }
            m_index = 0;
            return;
        }
        //すべて通っていたら次へ
        if (m_angleObject[m_index].GetComponent<CollisionDetection>().mIsEntered)
        {
            if(m_angleObject.Length-1 == m_index)
            {
                m_pointArray.mNext();
                return;
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

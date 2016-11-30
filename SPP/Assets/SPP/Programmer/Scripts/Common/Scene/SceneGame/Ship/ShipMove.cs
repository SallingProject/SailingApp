/**************************************************************************************/
/*! @file   ShipMove.cs
***************************************************************************************
@brief      船の動きを行うクラス
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ShipMove : BaseObject
{


    [System.Serializable]
    class CoefficientLift
    {
        [Range(0, 45)]
        public float m_direction_max;
        public AnimationCurve m_curve;
    }

    [SerializeField]
    private CoefficientLift m_cl;
    [SerializeField]
    private CoefficientLift m_cd;


    private WindObject m_wind;

    private float m_speedVector;
    private float m_surfacingRadian;

    private ShipDefine m_shipDefine;
    private float m_accelMagnification;

    [System.NonSerialized]
    public SailRotation m_sail;
    private RudderRotation m_rudder;

    //移動の際に発生した力
    public float mMoveForce
    {
        get; private set;
    }
    //定数

    private const float mkFriction = 0.98f;              //摩擦
    private const float mkNormalMagnification = 1.0f;
    private const float mkAirDensity = 1.2f;
    protected override void Start()
    {
        m_speedVector = 0;
        m_surfacingRadian = 0;
        m_wind = GameInfo.mInstance.m_wind;
        mNormalAccel();

        m_rudder = GetComponent<RudderRotation>();
        m_rudder.mHandling = m_shipDefine.mHandling;

        m_sail = GetComponentInChildren<SailRotation>();
    }


    public override void mOnUpdate()
    {
        ///*Test Code
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mItemActivate(ItemEffect.Boost);
        }
        //*/


        //Move
        mAcceleration();
        if (m_speedVector >= m_wind.mWindForce * (m_shipDefine.mMaxSpeed / 100) * m_accelMagnification)
        {
            m_speedVector = m_wind.mWindForce * (m_shipDefine.mMaxSpeed / 100) * m_accelMagnification;
        }

        m_speedVector *= mkFriction;
//        transform.Translate(new Vector3(0.0f, 0.0f, m_speedVector * Time.deltaTime));


        //FloatMove;
        m_surfacingRadian += Time.deltaTime * 120;
        transform.position = new Vector3(transform.position.x, Mathf.Sin(m_surfacingRadian / 180 * 3.14f) / 8, transform.position.z);
    }

    /****************************************************************************** 
    @brief      速度の加算　最大値を超えていた場合収めるが風力によって変わる    
    @note       MaxSpeed,Accelaration,
    *******************************************************************************/
    private void mAcceleration()
    {
        float liftForce = mLiftForce();
        float dragForce = mDragForce();
        float direction = 1;


        Vector3 force = new Vector3(liftForce, 0, dragForce);
        {
            Quaternion rote = Quaternion.AngleAxis(m_wind.mWindDirection, Vector3.up);
            float fl = transform.eulerAngles.y - m_wind.mWindDirection;
            if (fl > 180)
            {
                fl = fl - 360;
            }
            if (fl < 0)
            {
                direction = -1;
            }
            force.x *= direction;
            force = rote * force;

            GetComponentsInChildren<LineRenderer>()[0].SetPosition(0, transform.position);
            GetComponentsInChildren<LineRenderer>()[0].SetPosition(1, transform.position + force);
            //ベクトルの正射影
            Vector3 project = Vector3.Project(force, transform.right);
            GetComponentsInChildren<LineRenderer>()[1].SetPosition(0, transform.position);
            GetComponentsInChildren<LineRenderer>()[1].SetPosition(1, transform.position + project);
            force = force - project;

            GetComponentsInChildren<LineRenderer>()[2].SetPosition(0, transform.position);
            GetComponentsInChildren<LineRenderer>()[2].SetPosition(1, transform.position + force);
        }

        {
            Quaternion windrote = Quaternion.AngleAxis(-m_wind.mWindDirection, Vector3.up);
            Quaternion shiprote = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);
            Vector3 diff = force;
            diff = shiprote * windrote * diff;
            Debug.Log(diff);
            if (diff.z < 0){
                mMoveForce = 0;
                return;
            }
        }
        m_speedVector += Mathf.Abs(force.z) * (m_shipDefine.mAcceleration / 100) * m_accelMagnification;
        mMoveForce = force.z;

    }


    /****************************************************************************** 
    @brief      風を受けて力へ変える関数(抗力)
    @return     抗力
    *******************************************************************************/
    private float mDragForce()
    {
        float angle = mAngleAttack(m_wind.mWindDirection, m_sail.transform.eulerAngles.y);
        if (angle >= 90)
        {
            angle = 180 - angle;
        }
        float diff = angle / m_cd.m_direction_max;
        float cd = m_cd.m_curve.Evaluate(diff);
        //        Debug.Log("CD" + cd);

        float DragForce = (Mathf.Pow(m_wind.mWindForce, 2) * cd * mkAirDensity) / 2;
        //        Debug.Log("drag" + DragForce);
        if(DragForce > m_wind.mWindForce)
        {
            DragForce = m_wind.mWindForce;
        }
        return -DragForce;
    }


    /****************************************************************************** 
    @brief      風を受けて力へ変える関数(揚力）
    @return     揚力
    *******************************************************************************/
    private float mLiftForce()
    {
        //風の向きに対してセールが正しい向きをでない場合揚力は発生しない
        float shipFlagment = transform.eulerAngles.y - m_wind.mWindDirection;
        if (shipFlagment > 180)
        {
            shipFlagment = shipFlagment - 360;
        }
        float sailFlagment = m_sail.transform.eulerAngles.y - m_wind.mWindDirection;
        if (sailFlagment > 180)
        {
            sailFlagment = sailFlagment - 360;
        }
        Debug.Log("direction" + sailFlagment + "" + shipFlagment);
        //９０°辺りはその限りではないので無視させる
        if (Mathf.Abs(shipFlagment) < 90)
        {
            //進行方向とセールの向きが不一致かどうか
            if (sailFlagment < 0 && shipFlagment > 0 || sailFlagment > 0 && shipFlagment < 0)
            {
                //Debug.Log("Error: not Lift");
                return 0.0f;
            }
        }

        //揚力で計算してみる
        //まず迎え角を求める
        //揚力係数を疑似カーブから引っ張る
        float angle = mAngleAttack(m_wind.mWindDirection, m_sail.transform.eulerAngles.y);

        float diff = angle / m_cl.m_direction_max;
        float cl = m_cl.m_curve.Evaluate(diff);
        //        Debug.Log("CL" + cl);

        float LiftForce = (Mathf.Pow(m_wind.mWindForce, 2) * cl * mkAirDensity) / 2;
        //        Debug.Log("LiftForce" + LiftForce);

        return LiftForce;
    }
    /****************************************************************************** 
    @brief      迎え角を計算する
    @note       fluid   流体,0~360°    target  対象　transform.eulerAngle,
    @return     迎え角
    *******************************************************************************/
    private float mAngleAttack(float fluidDirec, float targetDirec)
    {
        Vector2 fluidVec, targetVec;
        fluidVec = SailMath.mDegToVector2(fluidDirec);
        targetVec = SailMath.mDegToVector2(targetDirec);
        //        Debug.Log("flued" + fluidVec);
        return Mathf.Acos(Vector2.Dot(fluidVec, targetVec)) * Mathf.Rad2Deg;
    }

    /****************************************************************************** 
    @brief      ScriptableObjectを受け取る
    @note       ShipCreateから呼ぶ
    *******************************************************************************/
    public void mSetShipDefine(ShipDefine define)
    {
        m_shipDefine = define;
    }

    /****************************************************************************** 
    @brief      風を受ける加速に変化をつける
    @note       Default 100(%) 
    *******************************************************************************/
    private void mTranslateAccel(float magnification, float time)
    {
        m_accelMagnification = magnification;
        StartCoroutine(mNormalWaitTime(time));
    }

    /****************************************************************************** 
    @brief      風を受ける加速を元に戻す
    *******************************************************************************/
    private void mNormalAccel()
    {
        m_accelMagnification = mkNormalMagnification;
    }

    /****************************************************************************** 
    @brief      効果時間待ち
    *******************************************************************************/
    private IEnumerator mNormalWaitTime(float time)
    {
        Debug.Log("Boost");
        yield return new WaitForSeconds(time);
        Debug.Log("Off");
        mNormalAccel();
    }


    /****************************************************************************** 
    @brief      タイプを渡されたら処理を行う
    @in         アイテムタイプ
@note       時間も渡すか検討    
    *******************************************************************************/
    public void mItemActivate(ItemEffect type)
    {
        switch (type)
        {
            case ItemEffect.Invalid:
                break;
            case ItemEffect.Boost:
                mTranslateAccel(2.0f, 3.0f);
                break;
            default:
                break;
        }
    }


}

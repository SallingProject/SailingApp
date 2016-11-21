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

public class ShipMove : BaseObject {


    private WindObject m_wind;

    private float m_speedVector;
    private float m_surfacingRadian;


    private ShipDefine m_shipDefine;

    private float m_accelMagnification = 100;
    //定数
    private const float mkFriction = 0.98f;              //摩擦

    protected override void Start()
    {
        m_speedVector = 0;
        m_surfacingRadian = 0;
        m_wind = GameInfo.mInstance.m_wind;
    }


    public override void mOnUpdate()
    {
        //仮コントロール
        float shipDirection = GameInfo.mInstance.mGetHandleRotation() * (m_shipDefine.mHandling / 100);

        mAcceleration();


        //FloatMove;
        m_surfacingRadian += Time.deltaTime * 120;
        transform.position = new Vector3(transform.position.x, Mathf.Sin(m_surfacingRadian / 180 * 3.14f) / 8, transform.position.z);

        //Rote
        transform.eulerAngles += Vector3.up * shipDirection*Time.deltaTime;
    }

    /****************************************************************************** 
    @brief      速度の加算　最大値を超えていた場合収めるが風力によって変わる    
    @note       MaxSpeed,Accelaration,
    @return     受けた風量
    *******************************************************************************/
    private void mAcceleration()
    {
        m_speedVector += (mForce(m_wind.mWindDirection) * m_wind.mWindForce) * 
            (m_shipDefine.mAcceleration/100) * (m_accelMagnification/100);

        if (m_speedVector >= m_wind.mWindForce * (m_shipDefine.mMaxSpeed/100))
        {
            m_speedVector = m_wind.mWindForce * (m_shipDefine.mMaxSpeed / 100);
        }

        m_speedVector *= mkFriction;
        transform.Translate(new Vector3(0.0f, 0.0f, m_speedVector * Time.deltaTime));

    }

    /****************************************************************************** 
    @brief      風を受けて力へ変える関数
    @note       風上へは一番力が弱くなる
    @return     受けた風量
    *******************************************************************************/
    private float mForce(float windDirec)
    {
        Vector2 windVec,shipVec;
        windVec.x = Mathf.Sin(windDirec / 180 * Mathf.PI);
        windVec.y = Mathf.Cos(windDirec / 180 * Mathf.PI);

        {   //船の向きをベクトルで取得
            Vector3 noVec = transform.forward;
            shipVec.x = noVec.x;
            shipVec.y = noVec.z;
        }

        //内積と１－ｘで90°以上で最速になるように
        float liftForce = Mathf.Abs(Vector2.Dot(windVec, shipVec));        
        liftForce = 1 - liftForce;


        //大きさは０～１に縮小する
        return Mathf.Clamp(liftForce, 0.0f, 1.0f);
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
    public void mTranslateAccel(float magnification)
    {
        m_accelMagnification = magnification;
    }

    /****************************************************************************** 
    @brief      風を受ける加速を元に戻す
    *******************************************************************************/
    public void mNormalAccel()
    {
        m_accelMagnification = 100;
    }



}

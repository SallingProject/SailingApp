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
    
    //定数
    private const float mkMagnification = 5.0f;             //最大速度倍率
    private const float mkFriction = 0.98f;              //摩擦
    public const float mkMoveValue = 2.0f;

    private float m_presaveDirection;
    protected override void Start()
    {
        m_speedVector = 0;
        m_surfacingRadian = 0;
        m_wind = GameInfo.mInstance.m_wind;
    }

    /****************************************************************************** 
    @brief      回転中かどうかの値を取得する
    @note       
    */
    public float mRotateValue { get; private set; }



    public override void mOnUpdate()
    {
        //仮コントロール
        float shipDirection = GameInfo.mInstance.mGetHandleRotationTrigger();
        if (shipDirection == 0) ;
        else StartCoroutine(mRotation(10, shipDirection));

        //速度の加算　最大値を超えていた場合収めるが風力によって変わる
        m_speedVector += mForce(m_wind.mWindDirection) * m_wind.mWindForce;
        if (m_speedVector >= m_wind.mWindForce * mkMagnification)
        {
            m_speedVector = m_wind.mWindForce * mkMagnification;
        }

        m_speedVector *= mkFriction;
        m_surfacingRadian += Time.deltaTime * 150;

        //移動
        transform.position = new Vector3(transform.position.x, Mathf.Sin(m_surfacingRadian / 180 * 3.14f) / 8, transform.position.z);
        transform.Translate(new Vector3(0.0f, 0.0f, m_speedVector * Time.deltaTime));
        //Rote
        //transform.eulerAngles += Vector3.up * shipDirection;
    }

    /****************************************************************************** 
    @brief      回転をTweenする
    @note       実行し終わったら消えます
    @return     none
    *******************************************************************************/
    private IEnumerator mRotation(int duration,float value)
    {
        mRotateValue = value / duration;
        while (duration >= 0)
        {
            transform.eulerAngles += Vector3.up * (mRotateValue);
            duration--;
            yield return null;
        }
        mRotateValue = 0;
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
        float liftForce = Vector2.Dot(windVec, shipVec);        
        liftForce = 1 - liftForce;

        //        Debug.Log( windVec+"ship"+shipVec);//+ "\tY:" + m_windDirectionY);
        //        Debug.Log(liftForce);

        //大きさは０～１に縮小する
        return Mathf.Clamp(liftForce, 0.0f, 1.0f);
    }




    
    	
}

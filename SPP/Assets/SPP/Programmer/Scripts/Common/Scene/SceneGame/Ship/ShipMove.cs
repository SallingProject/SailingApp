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

    [SerializeField]
    private GameInfo m_gameInfo;

    private WindObject m_wind;

    private float m_speedVector;
    private float m_surfacingRadian;


    private ShipDefine m_shipDefine;
    private float m_accelMagnification;

    //定数
    private const float mkFriction = 0.98f;              //摩擦
    private const float mkNormalMagnification = 1.0f;
    protected override void Start()
    {
        m_speedVector = 0;
        m_surfacingRadian = 0;
        m_wind = m_gameInfo.m_wind;
        mNormalAccel();
    }


    public override void mOnUpdate()
    {
        ///*Test Code
        if(Input.GetKeyDown(KeyCode.Q)){
            mItemActivate(ItemEffect.Boost);
        }
        //*/

        //仮コントロール
        float shipDirection = m_gameInfo.mGetHandleRotation() * (m_shipDefine.mHandling / 100);

        //LiftMove
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
        m_speedVector += (mForce(m_wind.mWindDirection,transform.eulerAngles.y) * m_wind.mWindForce) * 
            (m_shipDefine.mAcceleration/100) * m_accelMagnification;

        if (m_speedVector >= m_wind.mWindForce * (m_shipDefine.mMaxSpeed/100)*m_accelMagnification )
        {
            m_speedVector = m_wind.mWindForce * (m_shipDefine.mMaxSpeed / 100) * m_accelMagnification;
        }

        m_speedVector *= mkFriction;
        transform.Translate(new Vector3(0.0f, 0.0f, m_speedVector * Time.deltaTime));

    }

    /****************************************************************************** 
    @brief      風を受けて力へ変える関数(揚力じゃない計算）
    @note       風上へは一番力が弱くなる
    @return     受けた風量
    *******************************************************************************/
    public float mForce(float windDirec,float targetDirec)
    {
        Vector2 windVec,shipVec;
        windVec.x = Mathf.Sin(windDirec / 180 * Mathf.PI);
        windVec.y = Mathf.Cos(windDirec / 180 * Mathf.PI);

        shipVec.x = Mathf.Sin(targetDirec / 180 * Mathf.PI);
        shipVec.y = Mathf.Cos(targetDirec / 180 * Mathf.PI);

        //90°で最速になるように
        float liftForce = Vector2.Dot(windVec, shipVec);
        //スピン差し込む場所
        liftForce = Mathf.Abs(liftForce);

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
    private void mTranslateAccel(float magnification,float time)
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
                mTranslateAccel(2.0f,3.0f);
                break;
            default:
                break;
        }
    }


}

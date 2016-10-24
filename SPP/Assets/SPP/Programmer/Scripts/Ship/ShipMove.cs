using UnityEngine;
using System.Collections;

public class ShipMove : BaseObject {
    [SerializeField]
    [Range(-180,180)]
    private int m_windDirection;
    private float m_windForce;

    [SerializeField]
    private WindZone m_wind;

    private float m_speedVector;
    
    //定数
    private const float mkMaxSpeed = 20.0f;             //最大速度
    private const float mkFriction = 0.98f;              //摩擦

    protected override void Start()
    {
        m_windForce = 1;
        m_speedVector = 0;
    }


    public override void mOnUpdate()
    {
        float shipDirection = 0.0f;
        if(Input.GetKey(KeyCode.LeftArrow)){
            shipDirection += 1;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            shipDirection -= 1;
        }

        //速度の加算　最大値を超えていた場合収めるが風力によって変わる
        m_speedVector += mForce(m_wind.transform.rotation.y);
        if (m_speedVector >= mkMaxSpeed)
        {
            m_speedVector = mkMaxSpeed;
        }

        m_speedVector *= mkFriction;

        //移動
        transform.Translate(new Vector3(0.0f, 0.0f, m_speedVector * Time.deltaTime));
        //Rote
        transform.Rotate(new Vector3(0.0f, shipDirection, 0.0f));
        //        Debug.Log("X:"+m_windDirectionX + "\tY:" + m_windDirectionY);
        Debug.Log(m_speedVector);
    }



    /****************************************************************************** 
    @brief      風を受けて力へ変える関数
    @note       
    @return     受けた風
    */

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

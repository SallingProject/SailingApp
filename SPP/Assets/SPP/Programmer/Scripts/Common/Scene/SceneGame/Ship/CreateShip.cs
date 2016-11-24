/**************************************************************************************/
/*! @file   CreateShip.cs
***************************************************************************************
@brief      船を生成するクラス
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class CreateShip : BaseObject{

    protected override void mOnRegistered()
    {
        mUnregisterList(this);

        //GameManagerから受け取る
        var scripObj = Resources.Load("Data/Ship/ShipTest") as ShipDefine;
        var path = scripObj.mPath;

        var obj = Resources.Load(path);
        var instance = mCreate(obj) as GameObject;
        instance.transform.SetParent(transform);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localEulerAngles = Vector3.zero;

        GetComponent<ShipMove>().mSetShipDefine(scripObj);

    }

}

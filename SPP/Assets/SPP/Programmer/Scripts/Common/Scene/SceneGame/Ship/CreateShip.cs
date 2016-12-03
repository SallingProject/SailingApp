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
        var selectedShip = (EShipType)PlayerPrefs.GetInt(SaveKey.mShipKey);
        string shipData = "Data/Ship/";
        switch (selectedShip)
        {
            case EShipType.Class470:
                shipData += "Ship0004";
                break;
            //case EShipType.ClassLaser:
            //    shipData += "Ship0001";
            //    break;
            //case EShipType.Class49er:
            //    shipData += "Ship0002";
            //    break;
            //case EShipType.ClassRS_X:
            //    shipData += "Ship0003";
            //    break;
            default:
                shipData += "ShipTest";
                break;
        }

        var scripObj = Resources.Load(shipData) as ShipDefine;
        var path = scripObj.mPath;

        var obj = Resources.Load(path);
        var instance = mCreate(obj) as GameObject;
        instance.transform.SetParent(transform);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localEulerAngles = Vector3.zero;

        instance.GetComponentInChildren<SailRotation>().enabled = true;
        GetComponent<ShipMove>().mSetShipDefine(scripObj);

        var shipStatus = gameObject.GetComponent<ShipStatus>();
        shipStatus.mId = 1;
        shipStatus.mShip = GetComponent<ShipMove>();
    }
}

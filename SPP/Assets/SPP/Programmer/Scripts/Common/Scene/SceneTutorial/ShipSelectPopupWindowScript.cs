/**************************************************************************************/
/*! @file   ShipSelectPopupWindowScript.cs
***************************************************************************************
@brief      船選択時のポップアップに関する処理
***************************************************************************************
@author     Ryo Sugiyama
***************************************************************************************
* Copyright © 2016 RyoSugiyama All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShipSelectPopupWindowScript : PopupBase
{

    [SerializeField]
    private GameObject m_contens;
    [SerializeField]
    private EShipType m_type;

    public void OpenEnd()
    {
        m_contens.SetActive(true);
    }
    public void CloseEnd()
    {
        m_contens.SetActive(false);
    }    
    void PopupAction(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                PlayerPrefs.SetInt(SaveKey.mShipKey, (int)m_type);
                GameInstance.mInstance.mSceneLoad(new LoadInfo("InTutorial"));
                break;
        }
    }
    public void GetShipType(EShipType type)
    {
        m_type = type;
    }
    public void Open()
    {
       
        mButtonSet = EButtonSet.Set1;
        PopupButton.mOnClickCallback = PopupAction;
        base.Open(null,null , OpenEnd);
    
    }

    public void Close()
    {
       base.Close(null, null, CloseEnd);
    }

}

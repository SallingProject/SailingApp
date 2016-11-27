using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShipSelectPopupWindowScript : PopupBase {

    [SerializeField]
    private GameObject m_contens;

    void PopupAction(EButtonId id)
    {
        switch (id)
        {
            case EButtonId.Ok:
                GameInstance.mInstance.mSceneLoad(new LoadInfo("GameInfo"));
                break;
        }
    }
    
    public void Open()
    {
        mButtonSet = EButtonSet.Set1;
        PopupButton.mOnClickCallback = PopupAction;
        base.Open(null, null, OpenEnd);

    }
    public void Close()
    {
       base.Close(null, null, CloseEnd);
    }
    public void OpenEnd()
    {
        m_contens.SetActive(true);
    }
    public void CloseEnd()
    {
        m_contens.SetActive(false);
    }
}

/**************************************************************************************/
/*! @file   SceneDebugHome.cs
***************************************************************************************
@brief      デバッグシーンクラス
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;

public class SceneDebugHome : BaseObject {

    public void mGoToTitle()
    {
        GameInstance.mInstance.ChnageScene("Title");
    }

    public void mGoToLibrary()
    {
        GameInstance.mInstance.ChnageScene("Library");
    }

}

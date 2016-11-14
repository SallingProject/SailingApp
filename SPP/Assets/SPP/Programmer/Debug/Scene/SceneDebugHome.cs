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

public class SceneDebugHome : SceneBase {

    public void mGoToTitle()
    {
        GameInstance.mInstance.AsyncLoad("Title");
    }

    public void mGoToLibrary()
    {
        GameInstance.mInstance.AsyncLoad("Library");
    }

}

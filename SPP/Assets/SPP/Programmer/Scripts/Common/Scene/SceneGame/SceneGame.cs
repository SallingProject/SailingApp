/**************************************************************************************/
/*! @file   SceneGame.cs
***************************************************************************************
@brief      ゲームシーンの管理（Initializer）
***************************************************************************************
@author     Kaneko Kazuki
***************************************************************************************/

using UnityEngine;
using System.Collections;

public class SceneGame : SceneBase{

    /*
    とりあえずGameInstanceを持ってくる
    */
    [SerializeField]
    private Point[] m_point;
    [SerializeField]
    private PointCreater m_creater;

    protected override void mOnRegistered()
    {
        base.mOnRegistered();
    }

    protected override void Start()
    {
        //初期化したい順番ごとにクラスを追加していく

        foreach (var obj in m_point){
            obj.mInitializer();
        }
        m_creater.mInitializer();
        

    }
}

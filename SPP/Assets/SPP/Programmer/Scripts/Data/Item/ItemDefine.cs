﻿/**************************************************************************************/
/*! @file   ItemDefine.cs
***************************************************************************************
@brief      アイテムの定義
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
/************************************************************************************** 
@brief  アイテムの定義クラス
*/
public class ItemDefine : ScriptableObject {

    /************************************************************************************** 
    @brief  アイテム名
    */
    [SerializeField]
    string m_name;
    public string mName
    {
        get{return m_name;}
    }

    /************************************************************************************** 
    @brief  アイテムの画像のパス
    */
    [SerializeField]
    Sprite m_sprite;
    public Sprite mSprite
    {
        get { return m_sprite; }
    }

    /************************************************************************************** 
    @brief  アイテムの適用範囲
    */
    [SerializeField]
    ItemType m_type;
    public ItemType mType
    {
        get { return m_type; }
    }

    /************************************************************************************** 
    @brief  アイテムの効果
    */
    [SerializeField]
    ItemEffect m_effect;
    public ItemEffect mEffect
    {
        get { return m_effect; }
    }
}

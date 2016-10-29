/**************************************************************************************/
/*! @file   PointMarker.cs
***************************************************************************************
@brief      ポイントマーカーの管理クラス
***************************************************************************************
@author     Ko Hashimoto
***************************************************************************************
* Copyright © 2016 Ko Hashimoto All Rights Reserved.
***************************************************************************************/
using UnityEngine;
using System.Collections;

/**************************************************************************************
@brief  ターゲットマーカーの管理クラス  
*/
public class TargetMarker : BaseObject {

    /**************************************************************************************
    @brief  ターゲットマーカーのUIクラス  
    */
    [System.Serializable]
    class CanvasUI
    {
        public RectTransform _top;
        public RectTransform _bottom;
        public RectTransform _right;
        public RectTransform _left;
    }
    // キャンバスに表示するマーカーすべての親
    [SerializeField]
    private RectTransform m_canvasMarkRoot;

    [SerializeField]
    private CanvasUI m_canvasMark;

    // Spriteのマーカー
    [SerializeField]
    private GameObject m_spriteMark;
    
    // スプライトのオフセット
    [SerializeField]
    private Vector3 m_spriteOffset = Vector3.zero;

    // 次のポイント
    // 検証用にSerializeしているが
    // SetTargetで設定されることを想定している
#if UNITY_EDITOR
    [SerializeField]
#endif
    private GameObject m_target;

    // カメラに映っているかのコンポーネント
    private ReflectedOnCamera m_reflectedComponent;

    // 初期化処理
    protected override void mOnRegistered()
    {
        base.mOnRegistered();
#if UNITY_EDITOR
        m_reflectedComponent = m_target.GetComponent<ReflectedOnCamera>();
#endif
    }


    // なんかちらつくからその対策用
    //　実際の使用用途で消せるかも
    int m_updateCount = 0;
    /**************************************************************************************
    @brief  更新処理
    */
    public override void mOnLateUpdate()
    {
        // なんかちらつくから最初の2回は無視
        if(m_updateCount < 2)
        {
            m_updateCount += 1;
            return;
        }
        if (m_target == null || m_reflectedComponent == null) return;
        base.mOnUpdate();
        mSetSpriteMarkActive(m_reflectedComponent.mIsOnView);
        if (m_reflectedComponent.mIsOnView)
        {
            Vector3 position = m_target.transform.position;
            m_spriteMark.transform.position = position + m_spriteOffset;
        }
        else
        {
            Debug.Log(m_reflectedComponent.mGameCamera.transform.position);
            mSetCanvasMarkActive(m_target.transform.position, m_reflectedComponent.mGameCamera.transform.position);
        }
    }

    /**************************************************************************************
    @brief  マーカーのさすオブジェクトを設定
    */
    public void mSetTarget(GameObject nextPoint)
    {
        m_target = nextPoint;
        m_reflectedComponent = null;
        if (m_target != null)
        {
            var component = m_target.GetComponent<ReflectedOnCamera>();
            if (component != null)
            {
                m_reflectedComponent = component;
            }
        }
    }

    /**************************************************************************************
    @brief  ターゲットとカメラの位置関係からどのオブジェクトのアクティブをオンにするか決める
    */
    private void mSetCanvasMarkActive(Vector3 target,Vector3 camera)
    {
        Vector3 diff = target - camera;
        if (diff.x > 0)
        {
            m_canvasMark._right.SetActive(true);
            m_canvasMark._left.SetActive(false);
        }
        else
        {
            m_canvasMark._right.SetActive(false);
            m_canvasMark._left.SetActive(true);
        }
        m_canvasMark._top.SetActive(false);
        m_canvasMark._bottom.SetActive(false);
        //if(diff.x > diff.z)
        //{
        //    if(diff.x > 0)
        //    {
        //        m_canvasMark._right.SetActive(true);
        //        m_canvasMark._left.SetActive(false);
        //    }
        //    else
        //    {
        //        m_canvasMark._right.SetActive(false);
        //        m_canvasMark._left.SetActive(true);
        //    }
        //    m_canvasMark._top.SetActive(false);
        //    m_canvasMark._bottom.SetActive(false);
        //}
        //else if(diff.x < diff.z)
        //{
        //    if (diff.z > 0)
        //    {
        //        m_canvasMark._top.SetActive(true);
        //        m_canvasMark._bottom.SetActive(false);
        //    }
        //    else
        //    {
        //        m_canvasMark._top.SetActive(false);
        //        m_canvasMark._bottom.SetActive(true);
        //    }
        //    m_canvasMark._right.SetActive(false);
        //    m_canvasMark._left.SetActive(false);
        //}
    }

    /**************************************************************************************
    @brief  マーカーオブジェクトのアクティブ設定
    @note   true時はSpriteのActiveがtrueになって、frontはfalseになる
    */
    private void mSetSpriteMarkActive(bool isSpriteActive)
    {
        m_spriteMark.SetActive(isSpriteActive);
        
        if (isSpriteActive)
        {
            m_canvasMark._bottom.SetActive(false);
            m_canvasMark._top.SetActive(false);
            m_canvasMark._right.SetActive(false);
            m_canvasMark._left.SetActive(false);
        }

        m_canvasMarkRoot.SetActive(!isSpriteActive);

    }
}

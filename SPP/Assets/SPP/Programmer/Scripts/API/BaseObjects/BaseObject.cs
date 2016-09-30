/**********************************************************************************************/
/*! @file   BaseObject.cs
*********************************************************************************************
@brief      すべてのオブジェクトの基底クラス
*********************************************************************************************
@author     橋本 航
**********************************************************************************************/
using UnityEngine;
using System.Collections.Generic;

/******************************************************************************* 
@brief   すべてのオブジェクトの基底クラス
*/
public abstract class BaseObject: MonoBehaviour{
    
    #region メンバ変数、メンバ変数のアクセサー
    /******************************************************************************
    @brief      BaseObject型オブジェクト管理配列
    */
    static LinkedList<BaseObject> m_objectList = new LinkedList<BaseObject>();
    public static LinkedList<BaseObject> ObjectList
    {
        get { return m_objectList; }
        private set { m_objectList = value; }
    }

    /******************************************************************************
   @brief      自身の参照オブジェクト
   */
    private BaseObject m_owner;
    public BaseObject Owner
    {
        get { return m_owner; }
        private set { m_owner = value; }
    }

    /**************************************************************************************
    @brief      削除時の関数が呼ばれたかのフラグ。mOnDeleteが一度だけ呼ぶことを保証するため
    */
    private bool m_isCallDeleted = false;

    /**************************************************************************************
    @brief      BaseObjectを基底クラスとして作成されたコンポーネントの総数
    */
    private static int m_objectCount = 0;
    public static int ObjectCount
    {
        get { return m_objectCount; }
        private set { m_objectCount = value; }
    }
    #endregion

    #region MonoBehaviorによる実装のもの
    /****************************************************************************** 
    @brief      オブジェクト生成時に呼ばれる。MonoBehaviorの実装
    @note       スクリピティングランタイム適用のため、コーティングルールには従いません。
                基本的にオーバーライドせずmOnRegistered関数かStart関数をオーバーライドして使用してください。
                オーバーライドする場合は必ず派生先の先頭でbase.Awake()と記述してください。
    @return     none
    */
    protected virtual void Awake()
    {
        m_isCallDeleted = false;
        Owner = this;
        mRegisterList(this);
    }


    /****************************************************************************** 
    @brief      オブジェクトのアップデート前に呼ばれる。MonoBehaviorの実装
    @note       スクリピティングランタイム適用のため、コーティングルールには従いません。
    @return     none
    */
    protected virtual void Start(){ return; }

    /****************************************************************************** 
    @brief      アクティブオブジェクト削除時に呼ばれる。MonoBehaviorの実装
    @note       スクリピティングランタイム適用のため、コーティングルールには従いません。オーバーライドしないでください。
    @return     none
    */
    protected void OnDestroy()
    {

        Transform transform = this.gameObject.transform;

        foreach(var child in transform.GetChildrenComponentTo<BaseObject>())
        {
            if(child != null)
            {
                child.mDeleteCallback();
                mUnregisterList(child);
            }
        }

        mUnregisterList(this);
        this.mDeleteCallback();
    }
    #endregion

    #region 派生先でオーバーライドが可能なもの
    /******************************************************************************
    @brief      更新処理。継承先で必ず実装する。
    @return     none
    */
    public virtual void mOnUpdate() { return; }
    
    /****************************************************************************** 
    @brief      管理リストへ登録された時に1度だけ呼ばれる。オーバーライド可能
    @return     none
    */
    protected virtual void mOnRegistered() { return; }

    /****************************************************************************** 
    @brief     管理リストから外された時に1度だけ呼ばれる。オーバーライド可能
    @return     none
    */
    protected virtual void mOnUnregistered() { return; }

    /****************************************************************************** 
    @brief      削除実行時に1度だけ呼ばれる。オーバーライド可能
    @return     none
    */
    protected virtual void mOnDelete() { return; }

    #endregion

    #region 静的関数群
    /******************************************************************************
    @brief      削除実行時１度だけ呼ばれる。
    @note       一度実行されている場合は実行しない
    @return     none
    */
    private void mDeleteCallback()
    {
        if (m_isCallDeleted) return;
        m_isCallDeleted = true;
        mOnDelete();
        ObjectCount -= 1;
    }

    /****************************************************************************** 
    @brief      指定オブジェクトを管理リストの最後尾に追加する。
    @note       すでにあるオブジェクトは追加できません。
    @return     none
    */
    static public void mRegisterList(BaseObject input)
    {
        if (mSerch(input) != null) return;
        ObjectList.AddLast(input);
        input.mOnRegistered();
        ObjectCount += 1;
    }

    /****************************************************************************** 
    @brief      指定オブジェクトが管理リストの管理対象なら管理リストから外す
    @return     成功：管理から外したオブジェクト/失敗：null
    */
    static public void mUnregisterList(BaseObject input)
    {
        if (mSerch(input) == null) return;

        if (ObjectList.Remove(input))
        {
            input.mOnUnregistered();
        }
        return;
    }

    /****************************************************************************** 
    @brief      オブジェクト検索用
    @return     発見時：そのオブジェクトの参照/ないとき:null
    */
    static public BaseObject mSerch(BaseObject input)
    {
        var findObject = ObjectList.Find(input);
        if(findObject != null)
        {
            return findObject.Value;
        }
        return null;
    }

    /********************************************************************************************
    @brief      オブジェクト削除用。削除したいオブジェクトがBaseObject型なら管理リストからも削除
    @note       MonoBehaviorのDestroy関数は使用せずこの関数を使用してください。
    @return     none
    */
    static public void mDelete<T>(T remove) where T : UnityEngine.Object
    {
        if (remove == null) return;
        if (remove is BaseObject)
        {
            BaseObject removeIndex = (BaseObject)(object)remove;
            if (mSerch(removeIndex) != null)
            {
                mUnregisterList(removeIndex);
            }
        }
        Destroy(remove);
        return;
    }

    /****************************************************************************** 
    @brief      オブジェクト生成用。生成したオブジェクトがBaseObject型なら管理リストに登録
    @note       MonoBehaviorのInstantiate関数は使用しないでください。
    @return     成功時：生成したオブジェクト/失敗時:null
    */
    static public T mCreate<T>(T input) where T : UnityEngine.Object
    {
        if (input == null) return null;
        T output = Instantiate(input) as T;
        if (output is BaseObject)
        {
            mRegisterList((BaseObject)(object)output);
        }
        return output;
    }

    #endregion
}
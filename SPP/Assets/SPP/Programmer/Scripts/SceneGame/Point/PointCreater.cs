using UnityEngine;
using System.Collections;

public class PointCreater : BaseObject{

    [System.Serializable]
    class MakePoint
    {
        public GameObject m_obj = null;
        public Vector3 m_translate = Vector3.zero;
        public string m_name = "";
    }

    [SerializeField]
    private MakePoint[] m_pointList;

    protected override void mOnRegistered()
    {
        mUnregisterList(this);
        mUnregister();
    }   


    // Use this for initialization
    protected override void Start()
    {
        int count = 0;
        for(int i = 0; i<m_pointList.Length; i++)
        {
            var obj_cre = BaseObject.mCreate(m_pointList[i].m_obj);
            obj_cre.transform.position = m_pointList[i].m_translate;
            obj_cre.transform.name = m_pointList[i].m_name;
            if(i == 1)
            {
                obj_cre.GetComponent<Point>().enabled = false;
            }
            if(i == 2)
            {
                obj_cre.GetComponentInChildren<Point>().enabled = false;
            }
            count++;
            
        }    	
	}	
}

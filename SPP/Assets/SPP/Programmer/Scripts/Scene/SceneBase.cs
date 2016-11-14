using UnityEngine;
using System.Collections;

public class SceneBase : BaseObject {

    protected override void Awake()
    {
        base.Awake();
        mUnregisterList(this);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

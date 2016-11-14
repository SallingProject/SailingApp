using UnityEngine;
using System.Collections;

public class SceneSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameInstance.mInstance.ChnageScene("DebugHome");
	}
}

using UnityEngine;
using System.Collections;

public class PushPool : MonoBehaviour {
    public float time = 5f;
    [HideInInspector]
    public float deltaTime = 0;
	// Use this for initialization
	void Start () {
        deltaTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (deltaTime < time) {
            deltaTime += Time.deltaTime;
        }
        else {
            deltaTime = 0;
            this.InvokeObjDeList(gameObject, "pushPool");
        }
	}
}

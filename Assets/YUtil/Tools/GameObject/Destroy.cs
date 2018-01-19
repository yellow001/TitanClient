using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {
    public float time = 1;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, time);
	}
	
}

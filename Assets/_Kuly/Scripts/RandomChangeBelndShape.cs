using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChangeBelndShape : MonoBehaviour {

    public bool change = true;

    public SkinnedMeshRenderer render;

    public List<int> changeIDs;
    
    public float changeTime=0.1f;

    [Range(0.25f,10f)]
    public float LastMaxTime=0.25f;
    
    float intervalTime;

    float deltaTime=0;

	// Use this for initialization
	void Start () {
        if (render == null) {
            render = GetComponent<SkinnedMeshRenderer>();
        }

        changeTime = changeTime < 0.1f ? 0.1f : changeTime;
        intervalTime += changeTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (!change) { return; }

        if (deltaTime < intervalTime) {
            deltaTime += Time.deltaTime;
        }
        else {
            deltaTime = 0;
            intervalTime = Random.Range(0.25f, LastMaxTime) + changeTime;
            ChangeBlendShape();
        }
	}

    void ChangeBlendShape() {
        for (int i = 0; i < changeIDs.Count; i++) {
            int id = changeIDs[i];
            this.AddTimeEvent(changeTime * 0.5f, 
                () => {
                    this.AddTimeEvent(changeTime * 0.5f, null,
                        (t, p) => {
                            if (render != null&&change)
                                render.SetBlendShapeWeight(id, (1 - p) * 100);
                        });
                }, 
                (t, p) => {
                    if(render!=null&&change)
                        render.SetBlendShapeWeight(id, p * 100);
                });
        }
    }

    public void ResetValue() {
        for (int i = 0; i < changeIDs.Count; i++) {
            if (render != null) {
                render.SetBlendShapeWeight(changeIDs[i], 0);
            }
        }
    }
}

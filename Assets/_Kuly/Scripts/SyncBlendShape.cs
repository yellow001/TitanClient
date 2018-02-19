using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[ExecuteInEditMode]
public class SyncBlendShape : MonoBehaviour {
    SkinnedMeshRenderer m_currentRender;
    public SkinnedMeshRenderer targetRender;
    public int currentIndex;
    public int targetIndex;
	// Use this for initialization
	void Start () {
        m_currentRender = GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (targetRender == null) { return; }
        if (m_currentRender == null) { m_currentRender = GetComponent<SkinnedMeshRenderer>(); }

        m_currentRender.SetBlendShapeWeight(currentIndex, targetRender.GetBlendShapeWeight(targetIndex));

	}
}

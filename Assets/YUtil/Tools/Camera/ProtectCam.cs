using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectCam : MonoBehaviour {

    // When to update the camera?
    [System.Serializable]
    public enum UpdateMode {
        Update,
        FixedUpdate,
        LateUpdate
    }

    public UpdateMode mode;

    public bool visualiseInEditor;                  // toggle for visualising the algorithm through lines for the raycast in the editor
    public float closestDistance = 0.5f;            // the closest distance the camera can be from the target
    public string[] dontClipTags = new string[] { "Player" };           // don't clip against objects with this tag (useful for not clipping against the targeted object)

    private Transform m_Cam;                  // the transform of the camera
    public Transform m_Pivot;                // the point at which the camera pivots around
    //private Ray m_Ray = new Ray();                        // the ray used in the lateupdate for casting between the camera and the target
    private RaycastHit[] m_Hits;              // the hits between the camera and the target
    private RayHitComparer m_RayHitComparer;  // variable to compare raycast hit distances

    Vector3 pos;

    private void Start() {
        // find the camera in the object hierarchyW
        m_Cam = transform;

        // create a new RayHitComparer
        m_RayHitComparer = new RayHitComparer();
    }


    private void Update() {
        if (mode == UpdateMode.Update)
            ProtectCamera();
    }

    private void FixedUpdate() {
        if (mode == UpdateMode.FixedUpdate)
            ProtectCamera();
    }

    private void LateUpdate() {
        if(mode==UpdateMode.LateUpdate)
            ProtectCamera();
    }

    private void ProtectCamera() {
        m_Hits = Physics.RaycastAll(m_Pivot.position, m_Cam.position - m_Pivot.position, (m_Cam.position - m_Pivot.position).magnitude);

        Array.Sort(m_Hits, m_RayHitComparer);

        // set the variable used for storing the closest to be as far as possible
        float nearest = Mathf.Infinity;

        // loop through all the collisions
        pos = m_Cam.position;
        for (int i = 0; i < m_Hits.Length; i++) {
            //Debug.Log(m_Hits[i].distance);
            // only deal with the collision if it was closer than the previous one, not a trigger, and not attached to a rigidbody tagged with the dontClipTag
            if (m_Hits[i].distance < nearest && (!m_Hits[i].collider.isTrigger) &&
                !(m_Hits[i].collider.attachedRigidbody != null && MatchTag(m_Hits[i].collider.attachedRigidbody.tag))) {
                // change the nearest collision to latest
                nearest = m_Hits[i].distance;
                pos = m_Hits[i].point;
            }
        }

        m_Cam.position = pos;
    }

    bool MatchTag(string t) {

        for (int i = 0; i < dontClipTags.Length; i++) {
            if (t.Equals(dontClipTags[i])) {
                return true;
            }
        }
        return false;
    }


    // comparer for check distances in ray cast hits
    public class RayHitComparer : IComparer {
        public int Compare(object x, object y) {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }
}

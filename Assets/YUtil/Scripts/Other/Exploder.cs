using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Exploder : MonoBehaviour {
    public int lineRend_segmentCount = 10;
    public float FireStrength = 10;
    public Vector3 FireDir = new Vector3(10, 10, 10);
    LineRenderer lineRenderer;
    public float lineRend_segmentScale = 1;
    public Vector2 widthGrow=new Vector2(1,1);
    public Vector2 alphaGrow=new Vector2(1,1);

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (lineRenderer == null) {
            lineRenderer = GetComponent<LineRenderer>();
        }
        Throw2();
	}

    void Throw() {
        Vector3[] segments = new Vector3[lineRend_segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = transform.position;

        // The initial velocity
        Vector3 segVelocity = /*transform.up*/FireDir * FireStrength * Time.deltaTime;

        // reset our hit object
        //_hitObject = null;

        for (int i = 1; i < lineRend_segmentCount; i++) {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            float segTime = (segVelocity.sqrMagnitude != 0) ? lineRend_segmentScale / segVelocity.magnitude : 0;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit hit;
            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, lineRend_segmentScale)) {
                // remember who we hit
                //_hitObject = hit.collider;

                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics.gravity * (lineRend_segmentScale - hit.distance) / segVelocity.magnitude;
                // flip the velocity to simulate a bounce
                segVelocity = Vector3.Reflect(segVelocity, hit.normal);

                /*
                 * Here you could check if the object hit by the Raycast had some property - was
                 * sticky, would cause the ball to explode, or was another ball in the air for
                 * instance. You could then end the simulation by setting all further points to
                 * this last point and then breaking this for loop.
                 */
            }
            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else {
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply our simulations to the LineRenderer

        // Set the colour of our path to the colour of the next ball
        Color startColor = Color.white;
        Color endColor = startColor;
        startColor.a = alphaGrow.x;
        endColor.a = alphaGrow.y;

#if UNITY_5_5_OR_NEWER
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;

        lineRenderer.startWidth = widthGrow.x;
        lineRenderer.endWidth = widthGrow.y;
   
        lineRenderer.positionCount = lineRend_segmentCount;
#elif UNITY_5_3_8
        lineRenderer.SetColors(startColor, endColor);
        lineRenderer.SetWidth(widthGrow.x, widthGrow.y);
#endif



        for (int i = 0; i < lineRend_segmentCount; i++)
            lineRenderer.SetPosition(i, segments[i]);
    }

    void Throw2() {
        Vector3[] segments = new Vector3[lineRend_segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = transform.position;

        // The initial velocity
        Vector3 segVelocity = /*transform.up*/FireDir * FireStrength * Time.deltaTime;

        // reset our hit object
        //_hitObject = null;

        for (int i = 1; i < lineRend_segmentCount; i++) {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            //float segTime = (segVelocity.sqrMagnitude != 0) ? lineRend_segmentScale / segVelocity.magnitude : 0;
            float segTime = lineRend_segmentScale;

            // Add velocity from gravity for this segment's timestep
            segVelocity = segVelocity + Physics.gravity * segTime;

            // Check to see if we're going to hit a physics object
            RaycastHit hit;
            if (Physics.Raycast(segments[i - 1], segVelocity, out hit, lineRend_segmentScale)) {
                // remember who we hit
                //_hitObject = hit.collider;

                // set next position to the position where we hit the physics object
                segments[i] = segments[i - 1] + segVelocity.normalized * hit.distance;
                // correct ending velocity, since we didn't actually travel an entire segment
                segVelocity = segVelocity - Physics.gravity * (lineRend_segmentScale - hit.distance) / segVelocity.magnitude;
                // flip the velocity to simulate a bounce
                segVelocity = Vector3.Reflect(segVelocity, hit.normal);

                /*
                 * Here you could check if the object hit by the Raycast had some property - was
                 * sticky, would cause the ball to explode, or was another ball in the air for
                 * instance. You could then end the simulation by setting all further points to
                 * this last point and then breaking this for loop.
                 */
            }
            // If our raycast hit no objects, then set the next position to the last one plus v*t
            else {
                segments[i] = segments[i - 1] + segVelocity * segTime;
            }
        }

        // At the end, apply our simulations to the LineRenderer

        // Set the colour of our path to the colour of the next ball
        Color startColor = Color.white;
        Color endColor = startColor;
        startColor.a = alphaGrow.x;
        endColor.a = alphaGrow.y;
#if UNITY_5_5_OR_NEWER
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;

        lineRenderer.startWidth = widthGrow.x;
        lineRenderer.endWidth = widthGrow.y;

        lineRenderer.positionCount = lineRend_segmentCount;
#elif UNITY_5_3_8
        lineRenderer.SetColors(startColor, endColor);
        lineRenderer.SetWidth(widthGrow.x, widthGrow.y);
#endif

        for (int i = 0; i < lineRend_segmentCount; i++)
            lineRenderer.SetPosition(i, segments[i]);
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlash : MonoBehaviour {
    new Light light;
    public float waitTime = 0.1f;
    public float changeTime = 0.25f;
    float deltaTime = 0;
    bool flash = false;
    public float minIns = 0;
    public float maxIns = 8;

    // Use this for initialization
    void Start() {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
        if (light == null) { return; }

        if (flash) { return; }

        deltaTime += Time.deltaTime;
        if (deltaTime >= waitTime) {
            deltaTime = 0;
            flash = true;
            float ins = Random.Range(minIns, maxIns);
            float tempIns = light.intensity;
            this.AddTimeEvent(changeTime, () => flash = false,
            (t, p) => {
                light.intensity = Mathf.Lerp(tempIns, ins, p);
            });

        }
    }
}

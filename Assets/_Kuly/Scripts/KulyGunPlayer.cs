using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KulyGunPlayer : MonoBehaviour {
    public ParticleSystem particle;

    protected bool fire = false;
    // Use this for initialization
    public virtual void Start() {
        this.AddObjEventFun(gameObject, "Fire", Fire);
    }

    public void LateUpdate() {
        if (fire) {
            fire = false;
            FireLateUpdate();
        }
    }

    public void Fire(object[] args) {
        fire = true;
    }

    public virtual void FireLateUpdate() {
        particle.Simulate(1, true);
        particle.Play(true);
    }
}

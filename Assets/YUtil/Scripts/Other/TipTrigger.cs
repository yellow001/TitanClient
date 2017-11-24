using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TipTrigger : MonoBehaviour {
    public string targetTag = "Player";
    public string tip;
    public float time = 3f;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(targetTag)) {
            this.AddTip(tip,time);
        }
    }
}

using UnityEngine;
using System.Collections;

public class DesWithOtherSimple : MonoBehaviour {

    public GameObject[] desArray;

    private void OnDestroy() {
        if (desArray != null && desArray.Length > 0) {
            for (int i = 0; i < desArray.Length; i++) {
                Destroy(desArray[i]);
            }
        }
    }
}

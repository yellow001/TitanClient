using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlotManager : BaseManager<BasePlotManager> {
    public BasePlotDataAsset asset;

    public void SetPlotDataAsset(BasePlotDataAsset a) {
        asset = a;
    }

    public virtual void Play() {

    }
}

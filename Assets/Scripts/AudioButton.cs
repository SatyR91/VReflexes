using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : ButtonScript {

	public override void Begin()
    {
        timer.StartTimer();
        indicatorObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        active = true;
    }
}

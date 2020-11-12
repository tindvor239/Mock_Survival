using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : FillBar
{
    private new Camera camera;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 lookAtPosition = camera.transform.position - transform.position;
        transform.forward = lookAtPosition;
    }
}

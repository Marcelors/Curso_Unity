using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public AnimationCurve Curve;
    public bool Inverted;

    private Vector3 gemPosition;

    // Start is called before the first frame update
    void Start()
    {
        Curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.5f))
        {
            preWrapMode = WrapMode.PingPong,
            postWrapMode = WrapMode.PingPong
        };

        gemPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Inverted)
        {
            transform.position = new Vector3(gemPosition.x, gemPosition.y - Curve.Evaluate(Time.time), gemPosition.z);
        }
        else
        {
            transform.position = new Vector3(gemPosition.x, gemPosition.y + Curve.Evaluate(Time.time), gemPosition.z);
        }
    }
}

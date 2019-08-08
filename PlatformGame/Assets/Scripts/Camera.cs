using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector2 velocity;
    public Transform Target;

    public Vector2 SmoothTime;
    public Vector2 MaxLimit;
    public Vector2 MinLimit;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, Target.position.x, ref velocity.x, SmoothTime.x);
            float poxY = Mathf.SmoothDamp(transform.position.y, Target.position.y, ref velocity.y, SmoothTime.y);

            transform.position = new Vector3(posX, poxY, transform.position.z);

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, MinLimit.x, MaxLimit.x),
                Mathf.Clamp(transform.position.y, MinLimit.y, MaxLimit.y),
                transform.position.z
            );
        }
    }
}

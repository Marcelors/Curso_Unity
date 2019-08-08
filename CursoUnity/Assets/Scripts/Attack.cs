using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Speed;
    public float TimeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
}

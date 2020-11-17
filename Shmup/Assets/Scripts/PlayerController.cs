using System;
using UnityEngine;

[Serializable]
public class Boundary
{
    public float xMinimum, xMaximum, yMinimum, yMaximum;
}

public class PlayerController : MonoBehaviour
{
    public Mover MoverComponent;
    public float Speed;

    public Boundary boundary;


    private void Update()
    {
        MoverComponent.DoMove(new Vector3(Input.GetAxis("Horizontal") * Speed * Time.deltaTime, Input.GetAxis("Vertical") * Speed * Time.deltaTime, transform.position.z));

        //x: 8
        //y: 4.3
        float x = Mathf.Clamp(transform.position.x, boundary.xMinimum, boundary.xMaximum);
        float y = Mathf.Clamp(transform.position.y, boundary.yMinimum, boundary.yMaximum);
        transform.position = new Vector3(x,y,transform.position.z);
    }
}

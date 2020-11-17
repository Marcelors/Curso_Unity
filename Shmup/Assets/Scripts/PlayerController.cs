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
    public Transform ShootOrigin;
    public GameObject ShootPrefab;

    private void Start()
    {
        MoverComponent.Speed = Speed;
        InputProvider.onHasShoot += OnHasShoot;
        InputProvider.OnDirection += OnDirection;
    }

    private void OnDirection(Vector3 direction)
    {
        MoverComponent.Direction = direction;
    }

    private void OnHasShoot()
    {
        Instantiate(ShootPrefab, ShootOrigin, false);
    }

    private void Update()
    {
        //x: 8
        //y: 4.3
        float x = Mathf.Clamp(transform.position.x, boundary.xMinimum, boundary.xMaximum);
        float y = Mathf.Clamp(transform.position.y, boundary.yMinimum, boundary.yMaximum);
        transform.position = new Vector3(x,y,transform.position.z);
    }


}

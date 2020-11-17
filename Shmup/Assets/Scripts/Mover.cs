using UnityEngine;

public class Mover : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }
}

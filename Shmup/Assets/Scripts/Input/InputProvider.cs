using UnityEngine;

public static class InputProvider
{
    public delegate void HasShoot();
    public static event HasShoot onHasShoot;

    public delegate void Direction(Vector3 direction);
    public static event Direction OnDirection;

    public static void OnTriggerHasShoot()
    {
        onHasShoot?.Invoke();
    }

    public static void OnTriggerDirection(Vector3 value)
    {
        OnDirection?.Invoke(value);
    }

}

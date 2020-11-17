using UnityEngine;

public class InputKeyboardListener : MonoBehaviour, IInputeable
{
    public void GetDirection(Vector3 direction)
    {
        InputProvider.OnTriggerDirection(direction);
    }

    public void ShootPressed()
    {
        InputProvider.OnTriggerHasShoot();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            ShootPressed();
        }

        GetDirection(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), transform.position.z));
    }
}

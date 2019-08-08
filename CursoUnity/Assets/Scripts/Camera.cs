using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector2 velecity;
    private Transform player;

    private float cameraY = 1.5f;

    public float SmoothTimeX;
    public float SmoothTimeY;

    private float shakeTimer;
    private float shakeAmount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velecity.x, SmoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.position.y + cameraY, ref velecity.y, SmoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer >= 0f)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float timer, float amount)
    {
        shakeTimer = timer;
        shakeAmount = amount;
    }
}

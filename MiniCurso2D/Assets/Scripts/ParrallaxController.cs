using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxController : MonoBehaviour
{
    public Transform Background;

    public float Speed;

    private Transform cam;
    private Vector3 previewCamPosition;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        previewCamPosition = cam.position;
    }

    void LateUpdate()
    {
        float parrallaxX = previewCamPosition.x - cam.position.x;
        float bgTargetX = Background.position.x + parrallaxX;

        Vector3 bgPosition = new Vector3(bgTargetX, Background.position.y, Background.position.z);

        Background.position = Vector3.Lerp(Background.position, bgPosition, Speed * Time.deltaTime);

        previewCamPosition = cam.position;
    }
}

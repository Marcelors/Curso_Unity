using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform bg0;
    public Transform bg1;
    public Transform bg2;

    public float factorBg0 = 1f;
    public float factorBg1 = 1 /2f;
    public float factorBg2 = 1 /4f;

    private float displacement;
    private float iniCamPosFrame;
    private float nextCamPosFrame;
    // Update is called once per frame
    void Update()
    {
        iniCamPosFrame = transform.position.x;
        transform.position = new Vector3(Player.player.transform.position.x, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        nextCamPosFrame = transform.position.x;

        bg0.position = new Vector3(bg0.position.x + (nextCamPosFrame - iniCamPosFrame) * factorBg0, bg0.position.y, bg0.position.z);
        bg1.position = new Vector3(bg1.position.x + (nextCamPosFrame - iniCamPosFrame) * factorBg1, bg1.position.y, bg1.position.z);
        bg2.position = new Vector3(bg2.position.x + (nextCamPosFrame - iniCamPosFrame) * factorBg2, bg2.position.y, bg2.position.z);
    }
}

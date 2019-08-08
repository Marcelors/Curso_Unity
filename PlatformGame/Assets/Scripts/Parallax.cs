using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] Bgs;
    public float[] ParallaxVel;
    public float Smooth;

    public Transform Cam;

    private Vector3 PreviewCam;

    // Start is called before the first frame update
    void Start()
    {
        PreviewCam = Cam.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Bgs.Length; i++)
        {
            float parralax = (PreviewCam.x - Cam.position.x) * ParallaxVel[i];
            float targetPosX = Bgs[i].position.x - parralax;

            Vector3 targetPos = new Vector3(targetPosX, Bgs[i].position.y, Bgs[i].position.z);

            Bgs[i].position = Vector3.Lerp(Bgs[i].position, targetPos, Smooth);
        }

        PreviewCam = Cam.position;
    }
}

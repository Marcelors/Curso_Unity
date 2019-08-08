using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform PlayerTransform;
    private Camera cam;

    public float SpeedCam;

    public Transform LimiteCamEsq;
    public Transform LimiteCamDir;
    public Transform LimiteCamSup;
    public Transform LimiteCamBai;

    public AudioSource Sfx;
    public AudioSource MusicSource;

    public AudioClip SfxJump;
    public AudioClip SfxAtack;
    public AudioClip SfxCoin;
    public AudioClip SfxEnemyDead;
    public AudioClip SfxDamage;

    public AudioClip[] SfxStep;

    public GameObject[] Fase;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        foreach (var o in Fase)
        {
            o.SetActive(false);
        }

        Fase[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        CamController();
    }

    private void CamController()
    {
        float posCamX = PlayerTransform.position.x;
        float posCamY = PlayerTransform.position.y;

        if (cam.transform.position.x < LimiteCamEsq.position.x && PlayerTransform.position.x < LimiteCamEsq.position.x)
        {
            posCamX = LimiteCamEsq.position.x;
        }
        else if (cam.transform.position.x > LimiteCamDir.position.x && PlayerTransform.position.x > LimiteCamDir.position.x)
        {
            posCamX = LimiteCamDir.position.x;
        }



        if (cam.transform.position.y < LimiteCamBai.position.y && PlayerTransform.position.y < LimiteCamBai.position.y)
        {
            posCamY = LimiteCamBai.position.y;
        }
        else if (cam.transform.position.y > LimiteCamSup.position.y && PlayerTransform.position.y > LimiteCamSup.position.y)
        {
            posCamY = LimiteCamDir.position.y;
        }



        Vector3 posCam = new Vector3(posCamX, posCamY, cam.transform.position.z);



        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, SpeedCam * Time.deltaTime);
    }

    public void PlaySFX(AudioClip sfxClip, float volume)
    {
        Sfx.PlayOneShot(sfxClip, volume);
    }
}

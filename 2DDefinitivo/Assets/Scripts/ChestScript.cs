using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public Collider2D Collider;
    public Sprite[] ImagensObject;
    public bool Open;

    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }


    public void Interaction()
    {
        if (!Open)
        {
            Open = true;
            spriteRender.sprite = ImagensObject[1];
            Collider.enabled = false;
        }
        //else
        //{
        //    Open = false;
        //    spriteRender.sprite = ImagensObject[0];
        //}
    }
}

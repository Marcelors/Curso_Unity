using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public Collider2D Collider;
    public Sprite[] ImagensObject;
    public bool Open;
    public GameObject[] Loots;

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
            StartCoroutine(nameof(ChestScript.Loot));
            GetComponent<Collider2D>().enabled = false;
        }


        //else
        //{
        //    Open = false;
        //    spriteRender.sprite = ImagensObject[0];
        //}
    }

    IEnumerator Loot()
    {

        int qtdMoedas = Random.Range(1, 5);

        for (int l = 0; l < qtdMoedas; l++)
        {
            int idRand;
            var rand = Random.Range(0, 100);
            if(rand >= 75)
            {
                idRand = 0;
            } else
            {
                idRand = 0;
            }
            GameObject lootTemp = Instantiate(Loots[idRand], transform.position, transform.localRotation);
            lootTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-25, 25), 75));
            yield return new WaitForSeconds(0.1f);
        }
    }
}

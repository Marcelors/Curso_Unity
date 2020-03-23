using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageControl : MonoBehaviour
{
    public float[] DamageHelp;
    private GameController gameController;
    public GameObject KnockForcePrefab;
    public Transform KnockPosition;
    public float KnockX;
    private PlayerScript playerScript;
    private float kx;
    public bool LookLeft, PlayerLeft;
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;

        if (LookLeft == true)
        {
            float x = transform.localScale.x * -1;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xPlayer = playerScript.transform.position.x;

        if (xPlayer < transform.position.x)
        {
            PlayerLeft = true;
        }
        else if (xPlayer > transform.position.x)
        {
            PlayerLeft = false;
        }

        if (LookLeft && PlayerLeft)
        {
            kx = KnockX;
        }
        else if (!LookLeft && PlayerLeft)
        {
            kx = -1 * KnockX;
        }
        else if (LookLeft && !PlayerLeft)
        {
            kx = -1 * KnockX;
        }
        else if (!LookLeft && !PlayerLeft)
        {
            kx = KnockX;
        }
        KnockPosition.localPosition = new Vector3(kx, KnockPosition.localPosition.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Weapon":
                var weaponInfo = collision.gameObject.GetComponent<WeaponInfo>();
                float damage = weaponInfo.Damege;
                int damageType = weaponInfo.DamegeType;
                float damageTaken = damage + (damage * (DamageHelp[damageType] / 100));
                print("Tomei dano");

                GameObject knockTemp = Instantiate(KnockForcePrefab, KnockPosition.position, KnockPosition.localRotation);
                Destroy(knockTemp, 0.02f);
                break;
        }
    }

    private void Flip()
    {
        LookLeft = !LookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}

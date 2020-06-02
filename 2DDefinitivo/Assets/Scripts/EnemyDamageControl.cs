using System.Collections;
using UnityEngine;

public class EnemyDamageControl : MonoBehaviour
{
    [Header("Configuration Life")]
    public float EnemyLife;
    public float CurrentLife;
    public GameObject Bar;
    public GameObject DamageTxtPrefab;
    public Transform BarHP;
    private float percLife;

    public float[] DamageHelp;

    private GameController gameController;
    public GameObject KnockForcePrefab;
    public Transform KnockPosition;
    public float KnockX;
    private PlayerScript playerScript;
    private float kx;
    public bool LookLeft, PlayerLeft;

    private SpriteRenderer spriteRenderer;
    public Color[] CharacterColors;

    private bool isHit;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        playerScript = FindObjectOfType(typeof(PlayerScript)) as PlayerScript;

        if (LookLeft == true)
        {
            float x = transform.localScale.x * -1;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            float barX = Bar.transform.localScale.x * -1;
            Bar.transform.localScale = new Vector3(barX, Bar.transform.localScale.y, Bar.transform.localScale.z);
        }

        spriteRenderer.color = CharacterColors[0];
        Bar.SetActive(false);
        BarHP.localScale = new Vector3(1, 1, 1);

        CurrentLife = EnemyLife;
        percLife = 1;
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
                if (!isHit)
                {
                    Bar.SetActive(true);
                    isHit = true;
                    var weaponInfo = collision.gameObject.GetComponent<WeaponInfo>();
                    float damage = Random.Range(weaponInfo.DamegeMin, weaponInfo.DamegeMax + 1);
                    int damageType = weaponInfo.DamegeType;
                    float damageTaken = damage + (damage * (DamageHelp[damageType] / 100));

                    CurrentLife -= damageTaken;
                    percLife = CurrentLife / EnemyLife;
                    if (percLife < 0)
                    {
                        percLife = 0;
                    }

                    BarHP.localScale = new Vector3(percLife, 1, 1);

                    if (CurrentLife <= 0)
                    {

                        Destroy(this.gameObject);
                    }
                    else
                    {
                        GameObject damageTemp = Instantiate(DamageTxtPrefab, transform.position, transform.localRotation);
                        damageTemp.GetComponent<TextMesh>().text = damageTaken.ToString();
                        damageTemp.GetComponent<MeshRenderer>().sortingLayerName = "HUD";
                        int forcaX = 50;
                        if(PlayerLeft == false){
                            forcaX *= -1;
                        }
                        damageTemp.GetComponent<Rigidbody2D>().AddForce(new Vector3(forcaX, 200, 0));
                        Destroy(damageTemp, 1f);

                        GameObject knockTemp = Instantiate(KnockForcePrefab, KnockPosition.position, KnockPosition.localRotation);
                        Destroy(knockTemp, 0.02f);

                        StartCoroutine(nameof(EnemyDamageControl.Invulnerable));
                    }

                }
                break;
        }
    }

    private void Flip()
    {
        LookLeft = !LookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        float barX = Bar.transform.localScale.x * -1;
        Bar.transform.localScale = new Vector3(barX, Bar.transform.localScale.y, Bar.transform.localScale.z);
    }

    IEnumerator Invulnerable()
    {
        spriteRenderer.color = CharacterColors[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = CharacterColors[0];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = CharacterColors[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = CharacterColors[0];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = CharacterColors[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = CharacterColors[0];
        isHit = false;
        Bar.SetActive(false);
    }
}

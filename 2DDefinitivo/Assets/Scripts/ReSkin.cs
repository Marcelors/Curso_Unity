using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReSkin : MonoBehaviour
{
    private GameController gameController;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public string spriteSheetName;
    public string loadedSpriteSheetName;
    public bool isPlayer = false;

    private Dictionary<string, Sprite> spriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        if (isPlayer)
        {
            spriteSheetName = gameController.spriteSheetName[gameController.idPersonagem].name;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (isPlayer)
        {
            if(gameController.idPersonagem != gameController.idPersonagemAtual)
            {
                spriteSheetName = gameController.spriteSheetName[gameController.idPersonagem].name;
                gameController.idPersonagemAtual = gameController.idPersonagem;
            }
        }

        if (loadedSpriteSheetName != spriteSheetName)
        {
            LoadSpriteSheet();
        }

  

        spriteRenderer.sprite = spriteSheet[spriteRenderer.sprite.name];
    }

    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name, x => x);
        loadedSpriteSheetName = spriteSheetName;
    }
}

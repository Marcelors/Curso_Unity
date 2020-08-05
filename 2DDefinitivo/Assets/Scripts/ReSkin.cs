using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReSkin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public string spriteSheetName;
    public string loadedSpriteSheetName;

    private Dictionary<string, Sprite> spriteSheet;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if(loadedSpriteSheetName != spriteSheetName)
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

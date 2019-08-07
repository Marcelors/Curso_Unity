using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjeto : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ApagaObjeto", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApagaObjeto()
    {
        Destroy(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    public static Pop pop;

    private void Awake()
    {
        pop = this;
    }

    public void Show(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }

    public void Dissapear()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        pop = null;
    }
}

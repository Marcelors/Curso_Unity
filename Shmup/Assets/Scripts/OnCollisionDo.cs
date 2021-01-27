using UnityEngine;
using UnityEngine.Events;

public class OnCollisionDo : MonoBehaviour
{
    [SerializeField]
    private UnityEvent action;

    private GameObject obj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        obj = collision.gameObject;
        action.Invoke();
    }

    public void DestroyCollision()
    {
        if(obj != null)
        {
            Destroy(obj);
        }
    }
}

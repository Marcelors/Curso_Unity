using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public Mover MoverComponent;

    // Update is called once per frame
    private void Update()
    {
        var vector = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), transform.position.z);
        //MoverComponent.DoMove(vector);
    }
}

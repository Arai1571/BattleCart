using UnityEngine;

public class Bom : MonoBehaviour
{
    public float deleteTime=3.0f;

    void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}

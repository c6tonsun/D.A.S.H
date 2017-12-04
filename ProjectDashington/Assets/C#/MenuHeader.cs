using UnityEngine;

public class MenuHeader : MonoBehaviour
{
    public Vector3 offset;

    private void Awake()
    {
        if (offset == null)
        {
            offset = transform.position;
        }
    }
}

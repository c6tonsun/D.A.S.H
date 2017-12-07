using UnityEngine;

public class MenuHeader : MonoBehaviour
{
    public Vector2 offset;

    private void Awake()
    {
        if (offset == null)
        {
            offset = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}

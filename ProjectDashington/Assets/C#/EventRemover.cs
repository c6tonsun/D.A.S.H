using UnityEngine;

public class EventRemover : MonoBehaviour {

    private void OnEnable()
    {
        if (FindObjectsOfType<EventRemover>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}

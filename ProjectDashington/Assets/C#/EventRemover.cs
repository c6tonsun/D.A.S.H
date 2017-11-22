using UnityEngine;

public class EventRemover : MonoBehaviour {

    private void Start()
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

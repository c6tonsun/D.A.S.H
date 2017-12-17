using UnityEngine;

public class CreditHandler : MonoBehaviour {

    public int childCount = 3;
    private int _index;

    public void GoToCredits()
    {
        _index = -1;
        NextCredit();
    }

    public void NextCredit()
    {
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _index++;

        if (_index >= childCount)
        {
            _index = 0;
        }

        transform.GetChild(_index).gameObject.SetActive(true);
    }
}

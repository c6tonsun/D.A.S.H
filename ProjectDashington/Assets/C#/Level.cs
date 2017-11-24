using UnityEngine;

public class Level : MonoBehaviour {

    [SerializeField]
    private int _star;

    private int _levelNumber;

    private GameObject[] _diables;
    private Vector3[] _defaultPositions;
    private Vector3[] _defaultRotations;

    private void Awake()
    {
        CreateLevelNumber();

        FindDiables();
    }

    private void OnEnable()
    {
        EnableAllChildren();
        ResetDiables();
    }

    private void CreateLevelNumber()
    {
        char[] chars = name.ToCharArray();

        foreach (char c in chars)
        {
            // if number (0-9)
            if (c >= 48 && c <= 57)
            {
                // tens, hundreds...
                if (_levelNumber != 0)
                {
                    _levelNumber *= 10;
                }
                _levelNumber += c - 48;
            }
        }
    }

    private void FindDiables()
    {
        Health[] healths = GetComponentsInChildren<Health>(true);

        _diables = new GameObject[healths.Length];
        _defaultPositions = new Vector3[healths.Length];
        _defaultRotations = new Vector3[healths.Length];

        for (int i = 0; i < healths.Length; i++)
        {
            _diables.SetValue(healths[i].gameObject, i);
            _defaultPositions.SetValue(healths[i].transform.position, i);
            _defaultRotations.SetValue(healths[i].transform.localEulerAngles, i);
        }
    }

    private void EnableAllChildren()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(true);
        }
    }

    private void ResetDiables()
    {
        for (int i = 0; i < _diables.Length; i++)
        {
            _diables[i].transform.position = _defaultPositions[i];
            _diables[i].transform.localEulerAngles = _defaultRotations[i];

            if (_diables[i].GetComponent<RangeAOE>() != null)
            {
                _diables[i].GetComponent<RangeAOE>().ResetAnimation();
            }
            else if (_diables[i].GetComponent<MeleeAnimation>() != null)
            {
                _diables[i].GetComponent<MeleeAnimation>().ResetAnimation();
            }
        }
    }

    // Getters

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    public int GetStarValue()
    {
        return _star;
    }
}

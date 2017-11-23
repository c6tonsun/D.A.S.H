using UnityEngine;

public class RangeAOE : RangeAnimation {
    
    [SerializeField]
    private GameObject _needleParentObject;
    [SerializeField]
    private int _needleCount;
    [SerializeField]
    private float _distanceFromThis;
    [SerializeField]
    private float _needleSpeed;

    public bool _isNeedlesReady;
    private GameObject[] _needles;

    private void OnEnable()
    {
        //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        GetComponent<Animator>().runtimeAnimatorController =
            Resources.Load("Voodoo") as RuntimeAnimatorController;
        //transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    protected override void Start()
    {
        base.Start();

        if (_needleCount == 0)
        {
            _needleCount = 1;
        }

        _needles = new GameObject[_needleCount];
    }
    
    private void FixedUpdate () {

		if (!_isNeedlesReady && transform.localScale.x > 0.5f)
        {
            InstantiateNeedles();
            _isNeedlesReady = true;
        }

        if (_isNeedlesReady && transform.localScale.x > 0.58f)
        {
            LaunchNeedles();
        }

        if (transform.localScale.x == 0.5f)
        {
            _isNeedlesReady = false;
        }
	}

    private void InstantiateNeedles()
    {
        float angle = 360 / _needleCount;

        for (int i = 0; i < _needleCount; i++)
        {
            int childNumber = Random.Range(0, _needleParentObject.transform.childCount);
            GameObject needle = 
                Instantiate(_needleParentObject.transform.GetChild(childNumber).gameObject);

            Vector3 newPosition = transform.position;
            Vector3 newRotation = new Vector3(0, 0, angle * (i + 1));

            needle.transform.position = newPosition;
            needle.transform.eulerAngles = newRotation;

            needle.transform.Translate(-transform.up * _distanceFromThis);

            _needles[i] = needle;
        }
    }

    private void LaunchNeedles()
    {
        foreach (GameObject needle in _needles)
        {
            if (needle != null)
            {
                needle.GetComponent<Rigidbody2D>().AddForce(
                    -needle.transform.up * _needleSpeed, ForceMode2D.Impulse);
                Destroy(needle, 1f);
            }
        }
    }
}

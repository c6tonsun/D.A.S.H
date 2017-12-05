using UnityEngine;

public class CameraShake : MonoBehaviour {

    public bool shake;
    public int shakeTimes;
    public float shakeX;
    public float shakeY;
    private bool _positiveShake;
    private float _shakeCounter;
    private Transform _defaultTransform;

    private void Awake()
    {
        _defaultTransform = transform;
    }

    private void FixedUpdate()
    {
        if (shake)
        {
            Vector3 newPos = new Vector3(shakeX, shakeY, -10);
            if (_positiveShake)
            {
                newPos.x *= -1; 
                newPos.y *= -1;
                _positiveShake = false;
            }
            else
            {
                _positiveShake = true;
            }

            transform.position = newPos;

            _shakeCounter++;
            if (_shakeCounter > shakeTimes)
            {
                transform.position = _defaultTransform.position;
                _shakeCounter = 0;
                shake = false;
            }
        }
    }

    public void Shake()
    {
        shake = true;
        _shakeCounter = 0;
    }
}

using UnityEngine;

public class CameraShake : MonoBehaviour {

    public bool shake;
    public int shakeTimes;
    public float shakeX;
    public float shakeY;
    public float shakeZ;
    private int _shakeCounter;
    private int _totalShakeCounter;
    private Vector3 _defaultPos;
    private Vector3 _defaultRot;

    private void Awake()
    {
        _defaultPos = transform.position;
        _defaultRot = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        if (shake)
        {
            Vector3 newPos = new Vector3(shakeX, shakeY, -10);
            Vector3 newRot = new Vector3(0, 0, shakeZ);
            if (_shakeCounter > 2)
            {
                newPos.x *= -1; 
                newPos.y *= -1;
                newRot.z *= -1;
                _totalShakeCounter++;
                _shakeCounter = 0;
            }
            else
            {
                _shakeCounter++;
            }

            transform.position = newPos;
            transform.eulerAngles = newRot;
            
            if (_totalShakeCounter > shakeTimes)
            {
                transform.position = _defaultPos;
                transform.eulerAngles = _defaultRot;
                _totalShakeCounter = 0;
                shake = false;
            }
        }
    }

    public void Shake()
    {
        shake = true;
        _shakeCounter = 0;
        _totalShakeCounter = 0;
    }
}

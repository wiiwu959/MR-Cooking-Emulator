using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltShaker : MonoBehaviour
{
    [SerializeField]
    private GameObject _salt;
    [SerializeField]
    private float _coolDown = 0.3f;
    [SerializeField]
    private float _accelThreshold = 0.5f;
    [SerializeField]
    private Transform _spawnTransform;


    private Vector3 _lastPos;
    private Vector3 _lastVel;
    private float _timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0.0f && _DetectShaking())
        {
            _Spawn();
            _timer = _coolDown;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void _Spawn()
    {
        var salt = Instantiate(_salt, _spawnTransform.position, _spawnTransform.rotation);
        salt.transform.parent = transform;
    }

    private void LateUpdate()
    {
        _lastVel = (transform.position - _lastPos) / Time.deltaTime;
        _lastPos = transform.position;
    }

    private bool _DetectShaking()
    {
        var vel = (transform.position - _lastPos) / Time.deltaTime;
        var acceleration = vel - _lastVel;
        if (acceleration.magnitude > _accelThreshold)
            return true;

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EggShell : MonoBehaviour
{
    [SerializeField]
    private GameObject _halfEgg;
    [SerializeField]
    private GameObject _spawnedEgg;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private Vector3 _lastPos;
    private Vector3 _velocity;
    private bool _cracked;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent(typeof(Collider)) as Collider;
    }

    // Start is called before the first frame update
    void Start()
    {
        _Initialize();
    }

    private void LateUpdate()
    {
        _velocity = (transform.position - _lastPos) / Time.deltaTime;
        _lastPos = transform.position;
    }

    private void _Initialize()
    {
        _velocity = Vector3.zero;
        _halfEgg.transform.parent = transform;
        _cracked = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Enter " + collider.tag + " " + _velocity.sqrMagnitude);
        if (_cracked)
            return;

        if (_CheckCracking(collider))
        {
            _CrackEgg();
        }
    }

    private bool _CheckCracking(Collider collider)
    {
        // check tag
        if (collider.tag != "Pan")
            return false;

        // check velocity and direction
        //if (_velocity.sqrMagnitude < 1.0f)
        //    return false;

        //if (Vector3.Dot(_rigidbody.velocity, ) > -0.5f)
        //    return false;

        return true;
    }

    private void _CrackEgg()
    {
        SeperateTheEgg();
        StartCoroutine(_SpawnEgg());
        _cracked = true;
    }

    private IEnumerator _SpawnEgg()
    {
        yield return new WaitForSeconds(0.5f);

        Instantiate(_spawnedEgg, transform.position, _spawnedEgg.transform.rotation);
    }

    private void SeperateTheEgg()
    {
        _halfEgg.transform.parent = transform.parent;
        _rigidbody.useGravity = true;
        _halfEgg.GetComponent<Rigidbody>().useGravity = true;
    }
}

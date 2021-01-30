using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriedEggBehavior : MonoBehaviour
{
    private float _metallicValue;
    private float _smoothnessValue;
    private MeshRenderer _rend;
    private MeshRenderer _yolk;
    private Rigidbody _rb;
    private State _currentState;
    private float _wellTime;


    enum State
    {
        Rare,
        WellDone,
        OverWell,
        Overcooked
    }

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        _rend = this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        _yolk = this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>();
        _yolk.material.SetColor("_Color", new Color32(255, 195, 10, 0));

        _metallicValue = 0.8f;
        _smoothnessValue = 0.85f;
        _rb = gameObject.GetComponent<Rigidbody>();
        _currentState = State.Rare;
        _wellTime = 0.0f;

        _rend.material.SetFloat("_Metallic", _metallicValue);
        _rend.material.SetFloat("_Smoothness", _smoothnessValue);
    }


    // Update is called once per frame
    void Update()
    {
        _rb.WakeUp();
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Pan")
        {
            if (_currentState == State.Rare)
            {
                if (_smoothnessValue != 0.5f)
                {
                    _smoothnessValue -= 0.001f;
                    _rend.material.SetFloat("_Smoothness", _smoothnessValue);
                }

                if (_metallicValue != 0f)
                {
                    _metallicValue -= 0.001f;
                    _rend.material.SetFloat("_Metallic", _metallicValue);
                }

                if (_metallicValue <= 0f && _smoothnessValue <= 0.5f)
                {
                    _currentState = State.WellDone;

                    _rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    _rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    _rend.material.SetInt("_ZWrite", 1);
                    _rend.material.DisableKeyword("_ALPHATEST_ON");
                    _rend.material.DisableKeyword("_ALPHABLEND_ON");
                    _rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    _rend.material.renderQueue = -1;

                    _rend.material.SetColor("_Color", Color.white);
                }
            }

            if (_currentState == State.WellDone)
            {
                _wellTime += 0.01f;
                if(_wellTime >= 1f)
                {
                    _currentState = State.OverWell;
                    _wellTime = 0.0f;
                }
            }

            if (_currentState == State.OverWell)
            {
                _wellTime += 0.01f;
                _rend.material.SetColor("_Color", new Color(179, 129, 75));
                if (_wellTime >= 2f)
                {
                    _currentState = State.Overcooked;
                }
            }

            if (_currentState == State.Overcooked)
            {
                print(_currentState);

                _rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                _rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                _rend.material.SetInt("_ZWrite", 1);
                _rend.material.DisableKeyword("_ALPHATEST_ON");
                _rend.material.DisableKeyword("_ALPHABLEND_ON");
                _rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                _rend.material.renderQueue = -1;


                _rend.material.SetColor("_Color", new Color32(85, 45, 0, 0));
                _yolk.material.SetColor("_Color", new Color32(123, 101, 32, 0));
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pan")
            source.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pan")
            source.Stop();
    }
}

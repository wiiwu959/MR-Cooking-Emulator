using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacon : MonoBehaviour
{
    public int time = 5;
    public MeshRenderer _bacon;
    private Rigidbody _rb;
    enum State
    {
        Rare,
        WellDone,
        OverWell,
        Overcooked
    }
    private State _currentState;
    private int color_val;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        _rb = gameObject.GetComponent<Rigidbody>();
        //_bacon.material.SetColor("_Color", new Color(255, 255, 255));
        color_val = 255 * time;
    }
     
    // Update is called once per frame
    void Update()
    {
        _rb.WakeUp();
    }
    private int good_time = 600;
    private int init_time = 0;
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Pan")
        {
            _bacon.material.SetColor("_Color", new Color32( (byte)(color_val / time), (byte)(color_val / time),(byte)(color_val / time), 255));
            if (_currentState == State.Rare)
            {
                color_val--;
                if (color_val < 200)
                {
                    _currentState = State.WellDone;
                }
            }
            if (_currentState == State.WellDone)
            {
                init_time++;
                if(init_time > good_time)
                {
                    _currentState = State.OverWell;
                }
            }

            if (_currentState == State.OverWell)
            {
                color_val--;
                if (color_val < 70)
                {
                    _currentState = State.Overcooked;
                }
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

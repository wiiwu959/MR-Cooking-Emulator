using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeout;

    // Update is called once per frame
    void Update()
    {
        if (timeout > 0)
            timeout -= Time.deltaTime;
        else
            Destroy(gameObject);
    }
}

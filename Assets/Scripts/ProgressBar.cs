using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    const float SmoothProgressStep = 0.2f;
    private float ProgressSum = 0.0f;

    // The animated progress bar object
    [SerializeField]
    private Transform progressBar = null;

    [SerializeField]
    private MeshRenderer progress = null;

    // The message text used by the 'Visible' message style
    [SerializeField]
    private TextMeshPro messageText = null;

    // The progress text used by all non-'None' progress styles
    [SerializeField]
    private TextMeshPro progressText = null;


    void Start()
    {
        var scale = progressBar.localScale;
        scale.x = ProgressSum;
        progressBar.localScale = scale;
        progressText.text = "0%";
        messageText.text = "Salting";
    }

    public void IncrementByOne()
    {
        var scale = progressBar.localScale;
        if (scale.x <= 0.8f)
        {
            ProgressSum += SmoothProgressStep;
            scale.x = ProgressSum;
        }
        else {
            ProgressSum += SmoothProgressStep;
            progress.material.SetColor("_Color", Color.red);
        }
        progressBar.localScale = scale;
        progressText.text = ProgressSum * 100 + "%";

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            IncrementByOne();
        }
    }
}

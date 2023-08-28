using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPose : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        text.text = "Pose tidak terdeteksi";
    }

    public void PoseDetected()
    {
        text.text = "Pose gunting";
    }

    public void PoseNotDetected()
    {
        text.text = "Pose tidak terdeteksi";
    }
}

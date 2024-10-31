using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrasposition : MonoBehaviour
{
    public Transform cam3D;
    public Transform cam2D;

    public void FrontCam()
    {
        cam3D.gameObject.SetActive(true);
        cam2D.gameObject.SetActive(false);
    }

    public void SideCam()
    {
        cam2D.gameObject.SetActive(true);
        cam3D.gameObject.SetActive(false);
    }
}

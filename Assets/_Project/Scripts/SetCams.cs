using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCams : MonoBehaviour
{
    [SerializeField] private GameObject[] cams;

    private int idCurrentCam;

    private void Start()
    {
        SetNewCam();
    }


    [ContextMenu("SetNewCam")]
    public void SetNewCam()
    {
        for (int i = 0; i < cams.Length; i++)
        {
            if (i==idCurrentCam)
            {
                cams[i].SetActive(true);
            }
            else
            {
                cams[i].SetActive(false);
            }
        }

        if (idCurrentCam==cams.Length-1)
        {
            idCurrentCam=0;
        }
        else
        {
            idCurrentCam++;
        }
    
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer line;

    private Transform[] points;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void SetUpline(Transform[] points)
    {
        line.positionCount = points.Length;
        this.points = points;
    }
    
  
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i,points[i].position);
        }
    }
}

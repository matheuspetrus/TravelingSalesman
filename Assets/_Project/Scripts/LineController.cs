using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer line;

    private List<Transform>points;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    public void SetUpline(List<Transform> _points)
    {
        if (points!=null)
        {
            //points.Clear();
            points = new List<Transform>();
        }
        
        
        line.positionCount = _points.Count;
        points = _points;
    }
    
  
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i,points[i].position);
        }
    }
}

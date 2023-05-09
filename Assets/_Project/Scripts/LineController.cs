using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public LineRenderer line;

   [SerializeField] private List<Transform>points;

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

    public void Clear()
    {
        if (points!=null)
        {
            //points.Clear();
            points = new List<Transform>();
            line.positionCount=0;
        }

    }
  
    // Update is called once per frame
    void Update()
    {
        if (points.Count>0 && points!=null)
        {
            for (int i = 0; i < points.Count; i++)
            {
                line.SetPosition(i,points[i].position);
            }
        }
      
    }
}

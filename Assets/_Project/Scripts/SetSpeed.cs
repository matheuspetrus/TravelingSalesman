using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSpeed : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    
    // Start is called before the first frame update
 
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _slider.value; 
    }
}

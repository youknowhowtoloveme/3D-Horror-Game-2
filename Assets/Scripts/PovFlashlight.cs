using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PovFlashlight : MonoBehaviour
{

    Light m_Light;
  
    void Start()
    {
        m_Light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
           m_Light.enabled = !m_Light.enabled;
        }
    }
}

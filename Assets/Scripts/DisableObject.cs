using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Obj;
    public float activeTime;
    public bool triggerbased;

    private void Update()
    {
        if (Obj.active == true)
        {
            StartCoroutine(Disableobj());
        }
    }


    IEnumerator Disableobj()
    {
        yield return new WaitForSeconds(activeTime);
        Obj.SetActive(false);
    }

}

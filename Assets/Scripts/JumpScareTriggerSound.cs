using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpScareTriggerSound : MonoBehaviour
{
    public AudioSource ScarySound;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScarySound.Play();
            
            StartCoroutine(EndJump());
        }


    }



    IEnumerator EndJump()
    {
        yield return new WaitForSeconds(2);
        
    }
}
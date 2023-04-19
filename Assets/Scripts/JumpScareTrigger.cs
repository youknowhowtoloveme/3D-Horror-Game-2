using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpScareTrigger : MonoBehaviour
{
    public AudioSource ScarySound;
    [SerializeField] private Image customImage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScarySound.Play();
            customImage.enabled = true;
            StartCoroutine(EndJump());
        }

        
    }



    IEnumerator EndJump ()
    {
        yield return new WaitForSeconds(2);
        customImage.enabled = false;
    }
}

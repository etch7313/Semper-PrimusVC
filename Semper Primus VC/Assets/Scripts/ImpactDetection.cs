
using System;
using System.Collections;
using UnityEngine;

public class ImpactDetection : MonoBehaviour
{
    [SerializeField] private AudioSource hitSound;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            hitSound.Play();
            if (gameObject.transform.tag == "Target")
            {
                StartCoroutine(GetBackUp());
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    [SerializeField] private GameObject explosionEffect;
    public ParticleSystem effect;

    [SerializeField] private float delay = 3f;

    [SerializeField] private float explosionForce = 10f;

    [SerializeField] private float radius = 10f;
    public AudioSource fragSound;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode",delay);
        
    }

    private void Explode()
    {
        
        fragSound.Play();
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider near in colliders)
        {
            Rigidbody RigB = near.GetComponent<Rigidbody>();
            if(RigB!=null)
                RigB.AddExplosionForce(explosionForce,transform.position,radius,1f,ForceMode.Impulse);
        }
            
        
        effect.Play();
        fragSound.Play();
        StartCoroutine(Trig());
    }

    IEnumerator Trig()
    {
        yield return new WaitForSeconds(2f);
        GameObject effect2=Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(effect2);
    }
}

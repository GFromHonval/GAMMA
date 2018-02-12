using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject; //Creer l'objet hit qui refere a l'objet touche
        Health health = hit.GetComponent<Health>(); //Va chercher la vie de l'objet touche
        if (health != null)
            health.TakeDamage(10); //BIM les degats
        Destroy(gameObject);
    }
}

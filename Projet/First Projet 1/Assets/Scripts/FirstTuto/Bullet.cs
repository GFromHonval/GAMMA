using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float Damage;
    
    private void Start()
    {
        Damage = GameObject.Find("GameParameters").GetComponent<GameParameters>().DamageAttackedThisLevel;
        print(Damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject; //Creer l'objet hit qui refere a l'objet touche

        if (hit != null && hit.CompareTag("PlayerGirl") || hit != null && hit.CompareTag("PlayerBoy"))
        {
            if (hit.GetComponent<RotationPlayer>().LifePerso < Damage)
                hit.GetComponent<RotationPlayer>().LifePerso = 0;
            else
                hit.GetComponent<RotationPlayer>().LifePerso -= Damage;
        }
        
        Destroy(gameObject);
    }
}

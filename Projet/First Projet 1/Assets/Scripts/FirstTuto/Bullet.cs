﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject; //Creer l'objet hit qui refere a l'objet touche

        if (hit != null && hit.CompareTag("Player"))
        {
            if (hit.GetComponent<RotationPlayer>().GetLife < GameObject.Find("GameParameters").GetComponent<GameParameters>().DamageAttackedThisLevel)
                hit.GetComponent<RotationPlayer>().GetLife = 0;
            else
                hit.GetComponent<RotationPlayer>().GetLife -= GameObject.Find("GameParameters").GetComponent<GameParameters>().DamageAttackedThisLevel;
        }
        
        Destroy(gameObject);
    }
}

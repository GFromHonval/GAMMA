using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject; //Creer l'objet hit qui refere a l'objet touche

        if (hit != null && hit.CompareTag("Player"))
        {
            if (GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetLife < GameObject
                    .Find("GameParameters").GetComponent<GameParameters>().DamageAttackedThisLevel)
                GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetLife = 0;
            else
                GameObject.Find("GameLogic").GetComponent<PhotonNetworkManager>().GetLife -= GameObject.Find("GameParameters")
                    .GetComponent<GameParameters>().DamageAttackedThisLevel;
        }
        
        Destroy(gameObject);
    }
}

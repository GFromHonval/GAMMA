using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    //Float pour pouvoir changer les vitesses depuis Unity
    public float SpeedMovement; //4
    public float SpeedRotation; //350
    public float SpeedBullet;
	
    //Pour les balles
    public GameObject BulletPrefab;
    public Transform BulletSpawn;

    // Update is called once per frame
    void Update () {
		
        //Delacement du joueur

        if (!isLocalPlayer) //Controler que son joueur
            return;
		
		
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * SpeedRotation;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * SpeedMovement;

        transform.Rotate(0, x, 0);
        transform.Translate(0,0,z);
		
        //Condition de tir
        if (Input.GetKeyDown(KeyCode.Space))
            CmdFire();
    }

    //Pour tirer, Command pour que ca s'affiche dans le serveur
    [Command]
    void CmdFire()
    {
        //Creer la balle
        GameObject bullet = (GameObject) Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
		
        //Fait bouger la balle
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * SpeedBullet;
		
        //Spawn la balle sur le serv
        NetworkServer.Spawn(bullet);
		
        //Detruit la balle apres 2 sec
        Destroy(bullet, 2);
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.black;
    }
}

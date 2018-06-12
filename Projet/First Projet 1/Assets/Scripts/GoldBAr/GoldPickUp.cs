using UnityEngine;

namespace GoldBAr
{
	public class GoldPickUp : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other) //Tester si le joueur est sur un coin et lui ajouter la valeur du coin
		{
			GameObject.Find("PlateformeLoad").GetComponent<LoadBossLevel>().GetNotesCollected++;
			Destroy(gameObject); //enlever le coin du terrain
		}
	}
}

using UnityEngine;

namespace GoldBAr
{
	public class GoldPickUp : MonoBehaviour
	{
		public int NoteValue = 1000; //Valeur d'un Coin
	
		private void OnTriggerEnter(Collider other) //Tester si le joueur est sur un coin et lui ajouter la valeur du coin
		{
			if (!other.CompareTag("Player")) return;
			FindObjectOfType<GameManager>().AddGold(NoteValue);
			ScoreMaster.CurrentScoreNotes += NoteValue;
			Destroy(gameObject); //enlever le coin du terrain
		}
	}
}

using UnityEngine;

	public class ScoreMaster : MonoBehaviour {
	
	
	
		public static int CurrentScoreNotes;
		public int NoteValue = 1000;
		//private float OffSetY = 10;
		//private float sizeX = 125;
		//private float sizeY = 25;

		public void AddGold(int goldToAdd) //ajouter les golds sur le text sur l'ecran
		{
			CurrentScoreNotes += goldToAdd;
		}
		
        private void OnTriggerEnter(Collider other) //Tester si le joueur est sur un coin et lui ajouter la valeur du coin
		{
			if (!other.CompareTag("Player")) return;
			AddGold(NoteValue);
			Destroy(gameObject); //enlever le coin du terrain
		}

		private void OnGUI()
		{
			GUI.Box(new Rect(60, 10, 125, 20), "Score : " + CurrentScoreNotes);
		}

	}

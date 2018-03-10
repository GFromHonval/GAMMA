using UnityEngine;

	public class ScoreMaster : MonoBehaviour {
	
	
	
		public static int CurrentScoreNotes = 0;
		//private float OffSetY = 10;
		//private float sizeX = 125;
		//private float sizeY = 25;


		private void OnGUI()
		{
			GUI.Box(new Rect(60, 10, 125, 20), "Score : " + CurrentScoreNotes);
		}

	}

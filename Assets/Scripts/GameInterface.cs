using UnityEngine;
using System.Collections;

public class GameInterface : MonoBehaviour {
	
	public Tower[] towers = new Tower[6];
	Rect[] rectangles = new Rect[6];
	bool activeRightTowers = true;
	int towerActive;
	int cardW = 100;
	// Use this for initialization
	void Start () {
		// empezamos con la primera torre de la derecha seleccionada
		selectTower (0, 0);
		for (int i = 0; i<towers.Length; i++) {
			rectangles[i] = towers[i].getButton().initRectangle(i%3);	
		}
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < 3; i++) {
			if(rectangles[(activeRightTowers ? 0 : 1)*3 + i].Contains(Input.mousePosition) && Input.GetMouseButtonDown(0))
			{
				selectTower(activeRightTowers ? 0 : 1, i);
				towerActive = i;

			}
		}
	}

	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button ("Cambiar vista" , GUILayout.Width (cardW/2))) {	
			changeTowers();
		}
		for (int i = 0; i < 3; i++) {
			string text = "Torre" + i;
			if(activeRightTowers)
				text += " R";
			else
				text += " L";
			if (GUILayout.Button (text , GUILayout.Width (cardW))) {
				selectTower(activeRightTowers ? 0 : 1, i);
				towerActive = i;
			}
			towers[i].getButton().renderButton();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();


	}
	void selectTower(int i, int j)
	{
		towers [i*3 + towerActive].Disable ();
		towers [i*3 + j].Enable ();
	}
	void changeTowers()
	{
		// TO DO tengo que cambiar las torres cambiando tanto la camara como el atributo activeRightTowers y seleccionar la nueva torre activa
	}

	public bool OverGUI()
	{

		Vector2 mouse = Input.mousePosition;
		for (int i = 0; i < rectangles.Length; i++) 
		{
			if(rectangles[i].Contains(mouse))
				return true;
		}
		return false;
	}
}

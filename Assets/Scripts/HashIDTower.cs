using UnityEngine;
using System.Collections;

public class HashIDTower : MonoBehaviour {
	// ids de variables
	public int selectedID;
	public int shotID;
	public int heigthID;
	//ids de estados
	public int chargueState;
	public int standState;
	public int shotState;
	public int dischargueState;
	public int idleState;
	
	void Awake ()
	{
		selectedID = Animator.StringToHash("selected");
		shotID = Animator.StringToHash("shot");
		heigthID = Animator.StringToHash("heigth");
		//ids de estados
		chargueState = Animator.StringToHash("Base Layer.chargue");
		standState = Animator.StringToHash("Base Layer.stand");
		shotState = Animator.StringToHash("Base Layer.shot");
		dischargueState = Animator.StringToHash("Base Layer.dischargue");
		idleState = Animator.StringToHash("Base Layer.idle");
	}
}
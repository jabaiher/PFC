using UnityEngine;
using System.Collections;

public class archerClass : MonoBehaviour {

	GameObject arch;
	GameObject hand;
	public Vector3 translation;
	public Quaternion rotation;
	// Use this for initialization
	void Start () {
		//obtengo el animador del personaje
		Animator characterAnimator = this.GetComponent<Animator>();
		// mediante runtimeAnimatorController añado una instancia del controlador de arquero
		characterAnimator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animations/Controller/archer"));
		// busco el arco y lo instancio
		arch = (GameObject)Instantiate (Resources.Load<GameObject>("Models/Tower/Weapons/BW_CompositeBow/R1/BW_CompositeBow_R1"));
		// obtengo todas las manos
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("hand");
		//asigno la ultima mano que es la que coincide con nuestro objeto
		foreach (GameObject go in gameObjects) {
			if(go.transform.IsChildOf(transform))
			{
				hand = go;	
				break;
			}
		}
		//hand = gameObjects [gameObjects.Length - 1];
		//ajustar la rotacion segun el modelo
		switch (this.name) {
		case "Goblin(Clone)":
		case "Goblin": rotation = Quaternion.Euler(10, 90, 90);
			translation = new Vector3(-0.1f,0.04f,0);
			break;
		case "Knight(Clone)":
		case "Knight": rotation = Quaternion.Euler(-80, 90, 90);
			translation = new Vector3(-0.02f,-0.04f,0);
			break;
		}
		//muevo y roto el arco para encajar con la mano encontrada anteriormente
		arch.transform.parent = hand.transform ; //Parenting arch to the hand bone position
		arch.transform.localPosition = hand.transform.localPosition + translation;  // centering the arch
		arch.transform.localRotation = Quaternion.identity; //must point y local, so reset rotation
		arch.transform.localRotation = rotation; //and rotate the sword accordingly

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

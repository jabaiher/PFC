using UnityEngine;
using System.Collections;
public class Archer : Character {

	GameObject bow;
	GameObject quiver;
	GameObject bundle;

	// Use this for initialization
	new void Awake () {
		base.Awake();
		maxPower = 4000f;
	}

	// Update is called once per frame
	new void Update () {
		base.Update();
	}
	protected override void loadAnimator()
	{
		//obtengo el animador del personaje
		Animator characterAnimator = this.GetComponent<Animator>();
		// mediante runtimeAnimatorController añado una instancia del controlador de arquero
		characterAnimator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animations/Controller/archer"));
	}
	protected override void loadWeapons()
	{
		// cargar arco
		bow = (GameObject)Instantiate (Resources.Load<GameObject>("Prefabs/Bow"));
		//cargar prefab flecha en caso de no estar pasado
		if(projectilePrefab == null)
			projectilePrefab = Resources.Load<GameObject>("Prefabs/Arrow");
		//hand = gameObjects [gameObjects.Length - 1];
		//ajustar la rotacion segun el modelo
		switch (this.name) {
		case "Goblin(Clone)":
		case "Goblin": rotation = Quaternion.Euler(43.24676f, 81.36256f, 84.12553f);
			translation = new Vector3(0.08232605f,0.03431854f,0.01335945f);
			break;
		case "Knight(Clone)":
		case "Knight": rotation = Quaternion.Euler(330.1594f, 75.44682f, 96.11598f);
			translation = new Vector3(-0.03948795f,0.03671684f,-0.003569855f);
			break;
		}
		//muevo y roto el arco para encajar con la mano encontrada anteriormente
		bow.transform.parent = rightHand.transform ; //Parenting arch to the hand bone position
		bow.transform.localPosition = translation;  // centering the arch
		bow.transform.localRotation = Quaternion.identity; //must point y local, so reset rotation
		bow.transform.localRotation = rotation; //and rotate the sword accordingly

		// cargar carcaj
		quiver = (GameObject)Instantiate (Resources.Load<GameObject>("Models/Tower/Weapons/Quiver_01/R1/Quiver_01_R1"));
		//cargar monton flechas para carcaj
		bundle = (GameObject)Instantiate (Resources.Load<GameObject>("Models/Tower/Weapons/Arrow_01/BundleOfArrows_01_R1"));
		quiver.transform.localPosition = new Vector3 (0, 0.15f, 0);
		// monton de flechas dentro del carcaj
		bundle.transform.parent = quiver.transform;
		//ajustar la rotacion segun el modelo
		switch (this.name) {
		case "Goblin(Clone)":
		case "Goblin": rotation = Quaternion.Euler(0, 0, 15);
			translation = new Vector3(0.0042f,-0.12f,-0.093f);
			break;
		case "Knight(Clone)":
		case "Knight": rotation = Quaternion.Euler(7, 80, -801);
			translation = new Vector3(-0.1343f,0.031998f,0.10675f);
			break;
		}
		//muevo y roto el arco para encajar con la mano encontrada anteriormente
		quiver.transform.parent = leftShoulder.transform ; //Parenting arch to the hand bone position
		quiver.transform.localPosition = translation;  // centering the arch
		quiver.transform.localRotation = Quaternion.identity; //must point y local, so reset rotation
		quiver.transform.localRotation = rotation; //and rotate the sword accordingly

	}


	// metodos encargados de los eventos de animacion
	// se llama cuando se suelta la fleha para que salga disparada
	public override void shot (float power, Vector3 direction)
	{
		StartClock ();
		projectileReady.transform.parent = null;
		
		Rigidbody projectileRigidBodyTip = projectileReady.GetComponentInChildren<Rigidbody> ();
		projectileRigidBodyTip.isKinematic = false;
		
		Rigidbody projectileRigidBody = projectileReady.GetComponent<Rigidbody> ();
		projectileRigidBody.isKinematic = false;
		
		projectileRigidBody.AddForceAtPosition(direction * (power * maxPower),new Vector3(0,0,-0.8f));
		projectileReady = null;
		anim.SetTrigger (hashID.shotID);
	}
	// se llama cuando la mano llega a una flecha del carcaj
	public void PickProjectile()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		//si se ejecuta al cargar habra que cargar la flecha
		if (stateInfo.nameHash == hashID.chargueState) {
			//metodo que se ejecuta cuando se genera el evento en la animacion archer_chargue
			projectileReady = (GameObject)Instantiate (projectilePrefab); 
			Rigidbody projectileRigidBody = projectileReady.GetComponent<Rigidbody> ();
			projectileRigidBody.isKinematic = true;
			switch (this.name) {
			case "Goblin(Clone)":
			case "Goblin":
					rotation = Quaternion.Euler (342.8523f, 297.2261f, -0.4820175f);
					translation = new Vector3 (-0.8068208f, 0.2333372f, 0.3662202f);
					break;
			case "Knight(Clone)":
			case "Knight":
					rotation = Quaternion.Euler (314.5159f, 154.3253f, 316.5721f);
					translation = new Vector3 (0.3126586f, 0.5766041f, -0.5023098f);
					break;
			}
			projectileReady.transform.parent = leftHand.transform;
			projectileReady.transform.localPosition = translation;  // centering the arch
			projectileReady.transform.localRotation = Quaternion.identity; //must point y local, so reset rotation
			projectileReady.transform.localRotation = rotation; //and rotate the sword accordingly

		}
		//si se ejecuta al descargar se elimina la flecha
		if (stateInfo.nameHash == hashID.dischargueState) 
		{
			if (projectileReady != null) {
					Destroy (projectileReady);
			}
		}
	}
	// se llama en el momento que se empieza a estirar o soltar el arco
	public void ChargueBow()
	{
		//metodo que se ejecuta cuando se genera el evento en la animacion archer_chargue 
		//si se ejecuta al cargar se carga
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.nameHash == hashID.chargueState) 
		{
				bow.animation.Play ("chargue");
		}
		//si se ejecuta al descargar se descarga
		if (stateInfo.nameHash == hashID.dischargueState || stateInfo.nameHash == hashID.shotState)
		{
				bow.animation.Play ("unchargue");
		}
	}
	// se llama al principio de la animacion chargue
	public void Chargue()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.nameHash != hashID.dischargueState) 
		{
			StartSecundaryClock ();
		}
	}

}

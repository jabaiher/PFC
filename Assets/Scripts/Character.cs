using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AimController))]
public abstract class Character : MonoBehaviour {

	protected GameObject rightHand;
	protected GameObject leftHand;
	protected GameObject leftShoulder;
	protected GameObject projectileReady;
	protected HashIDTower hashID;
	protected Animator anim;
	protected ButtonTower button;
	public Texture2D imageButton;
	public Texture2D imageRechargeButton;
	public float maxPower = 1;
	public AimController aim;
	public GameObject projectilePrefab;
	public Vector3 translation;
	public Quaternion rotation;
	// Use this for initialization
	protected void Awake () {
		anim = GetComponent<Animator>();
		hashID = gameObject.AddComponent<HashIDTower>();
		loadAnimator ();
		// busco el arco y lo instancio
		// obtengo todas las manos
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("rightHand");
		//asigno la ultima mano que es la que coincide con nuestro objeto
		foreach (GameObject go in gameObjects) {
			if(go.transform.IsChildOf(transform))
			{
				rightHand = go;	
				break;
			}
		}
		// cargo la mano izquierda que luego utilizare para las flechas
		gameObjects = GameObject.FindGameObjectsWithTag("leftHand");
		//asigno la ultima mano que es la que coincide con nuestro objeto
		foreach (GameObject go in gameObjects) {
			if(go.transform.IsChildOf(transform))
			{
				leftHand = go;	
				break;
			}
		}
		// obtengo todos los hombros
		gameObjects = GameObject.FindGameObjectsWithTag("leftShoulder");
		//asigno el ultimo hombro que es la que coincide con nuestro objeto
		foreach (GameObject go in gameObjects) {
			if(go.transform.IsChildOf(transform))
			{
				leftShoulder = go;	
				break;
			}
		}

		loadWeapons ();
	}

	protected abstract void loadAnimator();
	protected abstract void loadWeapons ();
	public abstract void shot (float power, Vector3 direction);

	public Texture2D getImageButton(bool isCharged)
	{
		return isCharged ? imageButton : imageRechargeButton;
	}
	public void SetButton (ButtonTower button)
	{
		this.button = button;
	}
	// Update is called once per frame
	protected void Update () {
		this.Aim();
	}
	protected void Aim()
	{
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		//si se ejecuta al cargar habra que cargar la flecha
		if (stateInfo.nameHash == hashID.standState) {
			aim.activeBar = true;			
			anim.SetFloat(hashID.heigthID,aim.rotation/aim.maxAngle);
			
		} else {
			aim.activeBar = false;
		}
	}
	public void Enable()
	{
		anim.SetBool (hashID.selectedID, true);
	}
	public void Disable()
	{
		anim.SetBool (hashID.selectedID, false);
	}
	public bool IsSelected()
	{
		return anim.GetBool (hashID.selectedID);
	}
	public void StartClock()
	{
		button.StartRechargueTime();
	} 
	public void StartSecundaryClock()
	{
		button.StartChargueTime ();
	}
}

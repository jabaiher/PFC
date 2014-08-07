using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public enum characterClass {
		Archer, LongBowmanKamboom
	};
	public GameObject estructure;
	public float towerHeight;
	public GameObject characterModel;
	public characterClass job;
	public GameObject weapon;
	Character characterScript;
	ButtonTower button;
	AimController aim;

	// Use this for initialization
	void Awake()
	{
		estructure = (GameObject)Instantiate(estructure, transform.position, Quaternion.identity);
		estructure.transform.parent = this.transform;
		estructure.transform.rotation = this.transform.rotation;
		characterModel = (GameObject)Instantiate(characterModel, new Vector3(transform.position.x,transform.position.y + towerHeight,transform.position.z), Quaternion.identity);
		characterModel.transform.parent = estructure.transform;
		characterModel.transform.rotation = this.transform.rotation;
		characterScript = (Character)characterModel.AddComponent(job.ToString());
		aim = characterModel.AddComponent<AimController> ();
		characterScript.aim = aim;
		aim.SetCharacter (characterScript);
		button = characterModel.AddComponent<ButtonTower> ();
		button.SetCharacter (characterScript);
		characterScript.SetButton (button);

	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Enable()
	{
		characterScript.Enable();
	}
	public void Disable()
	{
		characterScript.Disable();
	}

	public ButtonTower getButton()
	{
		return button;
	}
}

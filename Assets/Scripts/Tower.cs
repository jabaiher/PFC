using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public GameObject estructure;
	public float towerHeight;
	public GameObject character;
	public string scriptClassPath;
	public GameObject weapon;
	// Use this for initialization
	void Start () {
		estructure = (GameObject)Instantiate(estructure, transform.position, Quaternion.identity);
		character = (GameObject)Instantiate(character, new Vector3(transform.position.x,transform.position.y + towerHeight,transform.position.z), Quaternion.identity);
		character.AddComponent(scriptClassPath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

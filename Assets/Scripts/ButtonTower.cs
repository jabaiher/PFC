using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class ButtonTower : MonoBehaviour {

	public Texture2D back;
	public Texture2D backSelected;
	public Texture2D rechargue;
	public Texture2D rechargueSelected;
	public Texture2D shiny;
	Rect rectangle;
	Character character;
	Texture2D imageTower;
	Texture2D imageRechargueTower;
	float gap = 20;
	float borderWidth;
	float borderHeight;
	public float rechargueTime = 3.16f;// tiempo usado para cargar desde que dispara hasta que puede apuntar de nuevo
	public float chargueTime = 2.55f; // tiempo usado en cargar desde idle
	float timeRemaining;
	float percent = 0.0f;
	bool isReady = true;
	// Use this for initialization
	void Start () {
		character = GetComponent<Character> ();
		borderWidth = Mathf.CeilToInt((back.width - backSelected.width) / 2.0f);
		borderHeight = Mathf.CeilToInt((back.height - backSelected.height)/2.0f);
		imageTower = character.getImageButton (true);
		imageRechargueTower = character.getImageButton (false);
		if(rectangle == null)
			rectangle = new Rect (gap, Screen.height - back.height - gap, back.width, back.height);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isReady)
		{
			// make sure the timer is not paused
			timeRemaining -= Time.deltaTime;
			percent = timeRemaining/rechargueTime;
			if (timeRemaining < 0)
			{
				timeRemaining = 0;
				isReady = true;
			}
		}
	}

	public void StartRechargueTime()
	{
		timeRemaining = rechargueTime;
		isReady = false;
	}

	public void StartChargueTime()
	{
		timeRemaining = chargueTime;
		isReady = false;
	}

	public void SetCharacter(Character ch)
	{
		character = ch;
	}

	public void renderButton()
	{
		GUI.BeginGroup (rectangle);
			GUI.DrawTexture (new Rect (0, 0, back.width, back.height), back);
			if (character.IsSelected()) {
					GUI.DrawTexture (new Rect (borderWidth, borderHeight, backSelected.width, backSelected.height), backSelected,ScaleMode.ScaleToFit);
				}
			GUI.DrawTexture (new Rect (borderWidth, borderHeight, imageTower.width, imageTower.height), imageTower,ScaleMode.ScaleToFit);
		GUI.BeginGroup (new Rect (borderWidth, borderHeight + rechargue.height - rechargue.height * percent, rechargue.width, rechargue.height ));
			GUI.DrawTexture (new Rect (0, -rechargue.height + rechargue.height * percent, rechargue.width, rechargue.height), rechargue,ScaleMode.ScaleToFit);	
				if (character.IsSelected ()) {
					GUI.DrawTexture (new Rect (0, -rechargue.height + rechargue.height * percent, backSelected.width, backSelected.height), rechargueSelected,ScaleMode.ScaleToFit);
				}
		GUI.DrawTexture (new Rect (0,  -rechargue.height + rechargue.height * percent , backSelected.width, backSelected.height ), imageRechargueTower,ScaleMode.ScaleAndCrop);
				GUI.EndGroup ();
		GUI.DrawTexture (new Rect (0, 0, shiny.width, shiny.height), shiny,ScaleMode.ScaleAndCrop);
		GUI.EndGroup ();
	}
	public Rect initRectangle(int position)
	{
		rectangle = new Rect (gap + position * (back.width + gap), Screen.height - back.height - gap, back.width, back.height);
		return new Rect (gap + position * (back.width + gap), gap, back.width, back.height);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Character))]
public class AimController : MonoBehaviour {

	float barMaxWidth;
	float barMaxHeight;
	float power = 3;
	float ratio;
	int newWidth;
	int newHeight;
	float distanceFactor = 1000.0f;
	Vector3 point;
	Vector3 mousePos;
	Vector2 centerPoint;
	Vector3 playerPosInScreen;
	public float maxAngle = 70;
	public float rotation = 0;
	Character character;
	GameInterface gameInterface;
	public float barSpeed = 1;
	public bool activeBar;
	public Texture2D powerArrow;
	public Texture2D backgroundArrow;
	bool canPress = false;
	// Use this for initialization
	void Start () 
	{
		gameInterface = GameObject.FindGameObjectWithTag ("game interface").GetComponent<GameInterface>();
		ratio = backgroundArrow.height/(float)backgroundArrow.width;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activeBar) 
		{
				Aim ();
		}
	}

	void Aim()
	{
		//rotation = Vector3.Angle (toMouseVector.normalized, axisXVector.normalized);
		mousePos = Input.mousePosition;
		playerPosInScreen = Camera.main.WorldToScreenPoint(this.transform.position);
		
		mousePos.x = mousePos.x - playerPosInScreen.x;
		mousePos.y = playerPosInScreen.y - mousePos.y ;
		// restringir el vector para evitar tiros hacia atras
		mousePos = mousePos.normalized;
		mousePos.x = Mathf.Abs (mousePos.x);
		mousePos.y = Mathf.Clamp (mousePos.y, -0.9f, 0.9f);
		// crear angulo de rotacion y limitarlo
		rotation = Mathf.Atan2 (-mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		rotation = Mathf.Clamp (rotation, -maxAngle, maxAngle);
		
		// compruebo que se ha pulsado fuera de cualquier boton y a partir de ahi a cargar
		if (!gameInterface.OverGUI() && Input.GetMouseButtonDown (0)) 
		{
			canPress = true;
		}
		// si can quiere decir que no he pulsado dentro de ningun elemento de interfaz
		if(canPress)
		{
			//Debug.Log ("Rotation: " + rotation + "Vector normalized:" + mousePos);
			if (activeBar) {
				if (Input.GetMouseButton (0)) {
					power += Time.deltaTime * 100 * barSpeed;
					power = Mathf.Clamp (power, 0, 100);
				} else {
					if (power != 3)
						character.shot (power/100.0f, -mousePos);
					power = 3;
				}
			}
		}
		// si he soltado el boton habre disparado y pongo can Press a false para reiniciar el control
		if (Input.GetMouseButtonUp (0)) 
		{
			canPress = false;
		}
	}

	void OnGUI()
	{
		if(activeBar)
		{
			point = Camera.main.WorldToScreenPoint (this.transform.position);
		
			newWidth = Mathf.RoundToInt ((1 / Mathf.Abs (point.z)) * distanceFactor);
			newHeight = Mathf.RoundToInt (ratio * newWidth);
			point.y = Screen.height - point.y;
			centerPoint = new Vector2 (point.x + newWidth * 0.05f  , point.y - newHeight * 0.25f);
			float newBarWidth = (power / 100) * newWidth; // this is the width that the foreground bar should be
			GUIUtility.RotateAroundPivot(-rotation, centerPoint);
			GUI.BeginGroup (new Rect (point.x, point.y - newHeight * 0.75f, newWidth, newHeight));
				GUI.DrawTexture (new Rect (0, 0, newWidth, newHeight), backgroundArrow);
				GUI.BeginGroup (new Rect (0, 0, newBarWidth, newHeight));
					GUI.DrawTexture (new Rect (0, 0, newWidth, newHeight), powerArrow);	
				GUI.EndGroup ();
			GUI.EndGroup ();
		}
	}

	public void SetCharacter(Character ch)
	{
		character = ch;
	}
}

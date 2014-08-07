using UnityEngine;
using System.Collections;

public class HashIDEnemy : MonoBehaviour {
	// ids de variables
	public int hitID;
	public int activeID;
	public int deadID;
	public int runID;
	//ids de estados
	public int runState;
	public int walkState;
	public int hitState;
	public int deadState;
	public int idleState;

	void Awake ()
	{
		hitID = Animator.StringToHash("hit");
		activeID = Animator.StringToHash("active");
		deadID = Animator.StringToHash("dead");
		runID = Animator.StringToHash("run");
		//ids de estados
		runState = Animator.StringToHash("Base Layer.run");
		walkState = Animator.StringToHash("Base Layer.walk");
		hitState = Animator.StringToHash("Base Layer.hit");
		deadState = Animator.StringToHash("Base Layer.dead");
		idleState = Animator.StringToHash("Base Layer.idle");
	}
}

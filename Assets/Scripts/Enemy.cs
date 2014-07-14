using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public Animator anim;
	public float live = 100.0f;
	public bool activeChar = true;
	int hitID = Animator.StringToHash("hit");
	int runState = Animator.StringToHash("Base Layer.run");
	int walkState = Animator.StringToHash("Base Layer.walk");
	
	void Start ()
	{
		anim = GetComponent<Animator>();
		anim.SetFloat ("live", live);
		anim.SetBool ("active", activeChar);
	}
	
	
	void Update ()
	{
		float move = Input.GetAxis ("Vertical");
		anim.SetFloat("Speed", move);
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.nameHash == runState)
		{

		}
	}

	void OnCollisionEnter(Collision col)
	{
		anim.SetTrigger (hitID);
	}

}

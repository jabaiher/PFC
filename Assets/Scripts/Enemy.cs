using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	//propiedades varias que habra que cambiar para cada enemigo

	public float live = 100.0f;
	public float speed = 1.2f;
	// aunque la clase sea Damage, se utiliza como resistencia
	// es una tonteria tener dos clases iguales solo por un cambio de nombre
	// resistencia de 1 igual a daño entero, resistencia menor 1 menos daño, resistencia mayor de 1 mas daño
	public Damage resistance = new Damage(1.0f,1.0f,1.0f,1.0f);
	public bool activeChar = true;
	Animator anim;
	HashIDEnemy hashID;
	Quaternion initRotation;

	void Awake ()
	{
		initRotation = this.transform.rotation;
		anim = GetComponent<Animator>();
		hashID = gameObject.AddComponent<HashIDEnemy>();
		anim.SetBool (hashID.activeID, activeChar);
	}
	
	void Update ()
	{
		
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if(stateInfo.nameHash == hashID.runState)
		{
			this.transform.Translate(Vector3.forward * Time.deltaTime * speed * 2);
		}
		if(stateInfo.nameHash == hashID.walkState)
		{
			this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
	}
	
	public void ReceiveDamage(Damage damage)
	{
		if (live > 0) {
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.nameHash != hashID.hitState) {
					// pongo el triguer comprobando no estar ya en hitstate para evitar lanzarlo de forma repetida
					anim.SetTrigger (hashID.hitID);
					this.transform.rotation = initRotation;
			}
			// pero aunque no lance la animacion el daño se lo come igualmente
			//calculamos el daño que nos hace teniendo en cuenta nuestra resistencia.
			live -= damage.calculateBasicDamage (resistance);

			if (live <= 0) {
					anim.SetBool (hashID.deadID, true);
			}
		}
	}

}

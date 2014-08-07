using UnityEngine;
using System.Collections;

public class Projectile: MonoBehaviour {

	public Damage damage;
	// esto lo pongo para probar, luego deberiamos quitarlo
	public float lightDamage;
	public float heavyDamage;
	public float magicDamage;
	public float critical = 1.5f;
	public Vector3 position;
	Rigidbody body;
	float flyingAngle;
	Quaternion rotationBeforeCollide;

	void Awake ()
	{
		damage = new Damage (lightDamage, heavyDamage, magicDamage, 0.0f);
		position = transform.position;
		body = GetComponent<Rigidbody>();
	}

	void Update()
	{

		StartCoroutine (debugPath());
	}

	void FixedUpdate()
	{
		if(rigidbody.velocity != Vector3.zero && !body.isKinematic){
			transform.LookAt(transform.position + (body.velocity * body.drag * Time.fixedTime));
			rotationBeforeCollide = transform.rotation;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		body.isKinematic = true;
		body.collider.enabled = false;
		this.transform.position = new Vector3 (1.01f * transform.position.x, transform.position.y, transform.position.z);
		this.transform.rotation = rotationBeforeCollide;
		this.transform.parent = col.gameObject.transform;
		if (col.gameObject.tag.ToString () == "enemy") 
		{
			col.gameObject.GetComponentInParent<Enemy>().ReceiveDamage(damage);
		}
		if (col.gameObject.tag.ToString () == "head") 
		{
			col.gameObject.GetComponentInParent<Enemy>().ReceiveDamage(damage * critical);
		}
	}

	IEnumerator debugPath()
	{
		Debug.DrawLine(position, transform.position  , Color.red,0.5f, false);
		position = transform.position;
		yield return new WaitForSeconds (0.5f);

	}
}

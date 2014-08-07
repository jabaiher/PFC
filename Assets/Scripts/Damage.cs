using UnityEngine;
using System.Collections;

public class Damage {

	public float light;
	public float heavy;
	public float magic;
	public float elemental;

	public Damage()
	{
		light = 0.0f;
		heavy = 0.0f;
		magic = 0.0f;
		elemental = 0.0f;

	}

	public Damage(float light, float heavyDamage, float magicDamage, float elementalDamage)
	{
		this.light = light;
		this.heavy = heavyDamage;
		this.magic = magicDamage;
		this.elemental = elementalDamage;
	}

	public static Damage operator +(Damage damage1, Damage damage2)
	{
		return new Damage (damage1.light + damage2.light,
		                  damage1.heavy + damage2.heavy,
		                  damage1.magic + damage2.magic,
		                  damage1.elemental + damage2.elemental);
	}

	public static Damage operator *(Damage damage1, Damage damage2)
	{
		return new Damage (damage1.light * damage2.light,
		                   damage1.heavy * damage2.heavy,
		                   damage1.magic * damage2.magic,
		                   damage1.elemental * damage2.elemental);
	}

	public static Damage operator *(Damage damage1, float multiplicator)
	{
		return new Damage (damage1.light * multiplicator,
		                   damage1.heavy * multiplicator,
		                   damage1.magic * multiplicator,
		                   damage1.elemental * multiplicator);
	}

	public float calculateBasicDamage(Damage damageResistance)
	{
		return this.light * damageResistance.light + this.heavy * damageResistance.heavy + this.magic * damageResistance.magic;
	}

	public float calculateElementalDamage(Damage damageResistance)
	{
		return elemental;
	}

}

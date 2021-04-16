using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	
	public string unitName;

	public Weapon weapon;
	public Armor armor;

	public float damage;

	public float maxHP;
	public float currentHP;
	
	public bool TakeDamage(Weapon fromWeapon)
	{
		float dmg = fromWeapon.calculateDamageGiven(armor);

		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}
	
}

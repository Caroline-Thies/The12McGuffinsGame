using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Enemy Unit", fileName="New Enemy Unit")]
public class Unit : ScriptableObject
{
	[Header("Appearance")]
	public Sprite sprite;
	public Sprite battleSprite;

	[Header("Stats")]
	public Weapon weapon;
	public Armor armor;
	public float maxHealth = 100.0f;
}

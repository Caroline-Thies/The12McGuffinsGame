using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
	public Text nameText;
	public Slider hpSlider;

	public void SetHud(Unit unit)
	{
		nameText.text = unit.unitName;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
	}

	public void SetHP(float hp)
	{
		hpSlider.value = hp;
	}
}

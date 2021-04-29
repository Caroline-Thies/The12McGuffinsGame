using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float lifeTimer = 0;
	
	private void Start()
	{
		Destroy(gameObject, lifeTimer);
	}
	
}

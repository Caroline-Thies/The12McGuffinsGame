﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupitem : MonoBehaviour
{
	public GameObject pickupEffect;
	
    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Player")
		{
			Instantiate(pickupEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
			
		}
	}
}

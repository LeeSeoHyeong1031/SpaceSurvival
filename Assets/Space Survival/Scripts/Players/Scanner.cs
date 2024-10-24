using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
	public LayerMask targetLayer;
	private float diff = 50;
	public Transform target;
	private void FixedUpdate()
	{
		Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, 50f, targetLayer);
		foreach (Collider2D coll in colls)
		{
			float distance = Vector3.Distance(coll.transform.position, transform.position);
			if (distance < diff)
			{
				diff = distance;
				target = coll.transform;
			}
		}
	}
}

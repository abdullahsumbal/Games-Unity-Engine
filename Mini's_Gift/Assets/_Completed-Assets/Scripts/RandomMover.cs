﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMover : MonoBehaviour {

	public Vector2 startWait;
	private float targetManeuver;
	public float dodge;
	public float smoothing;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	private Rigidbody rb;
	private float currentSpeed;
	public Boundary boundary;
	public float tilt;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (Evade());
		currentSpeed = rb.velocity.z;
	}

	IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true) {
			targetManeuver = Random.Range (1,dodge) *- Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range(maneuverWait.x, maneuverWait.y));
		
		}
	
	}
	// Update is called once per frame
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
			
	}
}

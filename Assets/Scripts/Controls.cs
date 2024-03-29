﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float MovementSpeed = 2f;
    public float GunRotation = 3f;

    private Transform _rotationPoint;

    void Start()
    {
        _rotationPoint = transform.GetChild(0);
    }

	void Update ()
	{
	    var x = Input.GetAxis("Horizontal");//A & D
	    var y = Input.GetAxis("Vertical");// S & W
	    transform.position += new Vector3(x, 0, 0) * MovementSpeed * Time.deltaTime;

        _rotationPoint.Rotate(new Vector3(0, 0,y), Time.deltaTime * GunRotation);

	    if (_rotationPoint.localEulerAngles.z > 90 && _rotationPoint.localEulerAngles.z < 180)
            _rotationPoint.localEulerAngles = new Vector3(0, 0, 90);
	    if (_rotationPoint.localEulerAngles.z < 270 && _rotationPoint.localEulerAngles.z >= 180)
	        _rotationPoint.localEulerAngles = new Vector3(0, 0, 270);

	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        var projectile = (GameObject) Instantiate(Resources.Load("Projectile"));
	        projectile.transform.rotation = Quaternion.Euler(0,0,_rotationPoint.eulerAngles.z - 90);
	        var projectileSpawn = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform;
	        projectile.transform.position = projectileSpawn.transform.position;
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.right * 500 * -1);
        }
	}
}

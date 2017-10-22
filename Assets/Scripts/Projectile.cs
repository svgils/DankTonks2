﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _previousPos;

    void Start()
    {
        _previousPos = transform.position;
    }

    void Update()
    {
        var newDir = Mathf.Atan2(transform.position.y -_previousPos.y, transform.position.x -
                                                                       _previousPos.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, newDir + 180);

        _previousPos = transform.position;

        if (transform.position.y < 0 || transform.position.x > TerrainManager.MaxX || transform.position.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        var other = collision.collider;

        if (other.tag == "Terrain")
        {
            var terrainmanager = (TerrainManager) other.gameObject.GetComponent(typeof(TerrainManager));
            terrainmanager.CalculateImpact(contact.point.x, contact.point.y, 1);
            Destroy(gameObject);
        }
        else if (other.tag == "Tank")
        {
            var tank = (TankControls)other.transform.parent.gameObject.GetComponent(typeof(TankControls));
            tank.DealDamage(10);
            Destroy(gameObject);
        }
    }

}

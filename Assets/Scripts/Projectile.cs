﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Projectile : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("contact");

        ContactPoint contact = collision.contacts[0];

        var other = collision.collider;

        if (other.tag == "Terrain")
        {
            var terrainmanager = (TerrainManager) other.gameObject.GetComponent(typeof(TerrainManager));
            terrainmanager.CalculateImpact(contact.point.x, contact.point.y, 1);
            Destroy(gameObject);
        }
    }
}

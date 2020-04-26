﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float speed = 10f;

    public float lifeTime = 1f;

    // Start is called before the first frame update
    void Start() {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update() {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}
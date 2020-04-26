using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameCamera : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;
    public float focusSpeed = 1f;
    
    // Start is called before the first frame update
    void Start() {
        transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * focusSpeed);
    }
}
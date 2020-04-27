using UnityEngine;

public class PatrollingLogic : MonoBehaviour {
    public Vector3[] directions;
    public float timeToChange = 1f;
    public float movementSpeed;
    private int directionPointer;
    private float directonTimer;

    /*
     * 0    1      2     3
     *Up, right, down, left
     * 
     */


    // Start is called before the first frame update
    void Start() {
        directionPointer = 0;

        directonTimer = timeToChange;
    }

    // Update is called once per frame
    void Update() {
        directonTimer -= Time.deltaTime;
        if (directonTimer <= 0f) {
            directonTimer = timeToChange;
            directionPointer++;
            if (directionPointer >= directions.Length) {
                directionPointer = 0;
            }
        }

        GetComponent<Rigidbody>().velocity = new Vector3(directions[directionPointer].x * movementSpeed,
                                                         GetComponent<Rigidbody>().velocity.y,
                                                         directions[directionPointer].z * movementSpeed);
    }
}
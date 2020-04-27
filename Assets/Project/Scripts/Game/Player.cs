using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour {
    public float health = 10;

    [Header("Visuals")] public GameObject model;
    public float rotatingSpeed = 2f;

    [Header("Equipment")] public Sword sword;
    public GameObject bombPrefab;
    public Bow bow;
    public float throwingSpeed = 10f;
    public int bombAmount = 3;
    public int arrowAmount = 15;

    [Header("Movement")] public float jumpingVelocity = 5f;
    public float movingVelocity = 5f;
    public float jumpingForce = 100f;
    private bool canJump = true;
    public float runSpeed = 3f;

    private Rigidbody playerRigidbody;
    public Quaternion targetModelRotation;
    public float knockBackSpeed = 100f;
    private float knockBackTimer;

    // Start is called before the first frame update
    void Start() {
        playerRigidbody     = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        // raycast to identify if the player can jump.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
            canJump = true;
        }

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, rotatingSpeed * Time.deltaTime);

        if (knockBackTimer > 0) {
            knockBackTimer -= Time.deltaTime;
        } else {
            ProcessInput();
        }
    }

    void ProcessInput() {
        playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);

        playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);

        if (Input.GetKey(KeyCode.W)) {
            playerRigidbody.velocity = new Vector3(
                                                   playerRigidbody.velocity.x,
                                                   playerRigidbody.velocity.y,
                                                   movingVelocity
                                                  );

            // visuals.transform.localEulerAngles = new Vector3(0,180,0);
            // visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation, Quaternion.Euler(0,180,0), rotatingSpeed * Time.deltaTime );
            targetModelRotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.S)) {
            playerRigidbody.velocity = new Vector3(
                                                   playerRigidbody.velocity.x,
                                                   playerRigidbody.velocity.y,
                                                   -movingVelocity
                                                  );

            // visuals.transform.localEulerAngles = new Vector3(0,0,0);
            // visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation, Quaternion.Euler(0,0,0), rotatingSpeed * Time.deltaTime );
            targetModelRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.A)) {
            playerRigidbody.velocity = new Vector3(
                                                   -movingVelocity,
                                                   playerRigidbody.velocity.y,
                                                   playerRigidbody.velocity.z
                                                  );

            // visuals.transform.localEulerAngles = new Vector3(0,90,0);
            // visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation, Quaternion.Euler(0,90,0), rotatingSpeed * Time.deltaTime );
            targetModelRotation = Quaternion.Euler(0, 270, 0);
        }

        if (Input.GetKey(KeyCode.D)) {
            playerRigidbody.velocity = new Vector3(
                                                   movingVelocity,
                                                   playerRigidbody.velocity.y,
                                                   playerRigidbody.velocity.z
                                                  );

            // visuals.transform.localEulerAngles = new Vector3(0,270,0);
            // visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation, Quaternion.Euler(0,270,0), rotatingSpeed * Time.deltaTime );
            targetModelRotation = Quaternion.Euler(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            canJump = false;

            // rigidbody.AddForce(0, jumpingForce, 0);
            playerRigidbody.velocity = new Vector3(
                                                   playerRigidbody.velocity.x,
                                                   jumpingVelocity,
                                                   playerRigidbody.velocity.z
                                                  );
        }

        // check requipment interraction
        if (Input.GetMouseButtonDown(0)) {
            sword.Attack();
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            ThrowBomb();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            arrowAmount = bow.Attack(arrowAmount);
        }
    }

    private void ThrowBomb() {
        if (bombAmount > 0) {
            GameObject bombObject = Instantiate(bombPrefab);
            bombObject.transform.position = transform.position + model.transform.forward;

            Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;
            bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);
            bombAmount--;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyBullet>() != null) {
            Hit((transform.position - other.transform.position).normalized);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<Enemy>() != null) {
            Hit((transform.position - other.transform.position).normalized);
        }
    }

    private void Hit(Vector3 direction) {

        Vector3 knockBackDirection = (direction + Vector3.up).normalized;
        playerRigidbody.AddForce(knockBackDirection * knockBackSpeed);
        knockBackTimer = 0.5f;
        health--;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
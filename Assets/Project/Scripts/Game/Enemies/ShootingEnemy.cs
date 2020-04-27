using UnityEngine;

public class ShootingEnemy : Enemy {
    public GameObject bulletPrefab;
    public float timeToShoot = 1f;
    private float shootingTimer;

    public GameObject model;
    public float timeToRotate = 2f;
    public float rotationSpeed = 6f;

    private Quaternion targetRotation;
    private int targetAngle;
    private float rotationTimer;


    // Start is called before the first frame update
    void Start() {
        rotationTimer = timeToRotate;
        shootingTimer = timeToShoot;
    }

    // Update is called once per frame
    void Update() {
        rotationTimer -= Time.deltaTime;
        if (rotationTimer <= 0f) {
            rotationTimer =  timeToRotate;
            targetAngle   += 90;
        }

        transform.localRotation =
            Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, targetAngle, 0), Time.deltaTime * rotationSpeed);

        // shoot bullet
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0f) {
            shootingTimer = timeToShoot;

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + model.transform.forward;
            bullet.transform.forward  = model.transform.forward;
        }
    }
}
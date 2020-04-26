using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour {
    public float swingingSpeed = 2f;
    public float cooldownSpeed = 2f;
    public float cooldownDuration = 0.5f;
    public float attackDuration = 0.35f;

    private bool isAttacking;

    private Quaternion targetRotation;
    private float cooldownTimer;
    private Collider collider;

    // Start is called before the first frame update
    void Start() {
        targetRotation = Quaternion.Euler(0, 0, 0);
        collider       = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update() {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation,
            Time.deltaTime * (isAttacking ? swingingSpeed : cooldownSpeed));

        if (cooldownTimer > 0) {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void Attack() {
        if (cooldownTimer > 0f) {
            return;
        }

        collider.isTrigger = true;
        targetRotation     = Quaternion.Euler(90, 0, 0);

        cooldownTimer = cooldownDuration;

        StartCoroutine(CooldownWait());
    }

    private IEnumerator CooldownWait() {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        collider.isTrigger = false;
        isAttacking        = false;
        targetRotation     = Quaternion.Euler(0, 0, 0);
    }
}
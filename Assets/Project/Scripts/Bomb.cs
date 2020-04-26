using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour {
    /**
     * Time variable while explosions the bomb.
     */
    public float explosionDuration = 0.25f;

    public float duration = 5f;
    public float radius = 3f;
    public GameObject explosionModel;

    private bool exploded;

    private float explosionTimer;


    // Start is called before the first frame update
    void Start() {
        explosionTimer = duration;

        // set explosion effect radius.
        explosionModel.transform.localScale = Vector3.one * radius;

        // explosion is disabled at the time        
        explosionModel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        explosionTimer -= Time.deltaTime;

        if (explosionTimer <= 0f) {
            exploded = true;

            // get the objects which touched on the bomb.
            Collider[] hitObjects = Physics.OverlapSphere(explosionModel.transform.position, radius);

            foreach (Collider collider in hitObjects) {
                if (collider.GetComponent<Enemy>() != null) {
                    collider.GetComponent<Enemy>().Hit();
                }
            }

            StartCoroutine(ExplodeBomb());
        }
    }

    /**
     * Trigger the bomb explosion.
     */
    private IEnumerator ExplodeBomb() {
        explosionModel.SetActive(true);
        yield return new WaitForSeconds(explosionDuration);
        Destroy(gameObject);
    }
}
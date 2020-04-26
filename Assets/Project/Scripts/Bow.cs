using UnityEngine;

public class Bow : MonoBehaviour {
    public GameObject arrowPrefab;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
    }

    public int Attack(int arrowAmount) {
        if (arrowAmount > 0) {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = transform.position + transform.forward;
            arrowAmount--;
        }

        return arrowAmount;
    }
}
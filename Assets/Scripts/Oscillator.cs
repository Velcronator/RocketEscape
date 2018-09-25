using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {


    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 4f;

    float movementFactor;

    Vector3 startingPos;

	// Use this for initialization
	void Start ()
    {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (period <= Mathf.Epsilon) { return; }
        float rawSineWav = Mathf.Sin((Time.time / period) * (2 * Mathf.PI)); //Cycles * Tau

        movementFactor = 0.5f + (rawSineWav / 2f); // Add a half to make it go between 0 and 1 instead of -0.5 and 0.5
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}

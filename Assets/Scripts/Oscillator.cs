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
        float cycles = Time.time / period;
        const float tau = 2 * Mathf.PI;
        float rawSineWav = Mathf.Sin(cycles * tau);

        movementFactor = 0.5f + (rawSineWav / 2f);
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
	}
}

#pragma strict

var bounceFrequency = 100;
var bounceForce = 5;
var sinceLastBounce : float = 0;

function Update () {
//	var delta : float = Time.deltaTime;
	sinceLastBounce += Time.deltaTime;
//	Debug.Log("Delta: " + delta + ", sinceLastBounce: " + sinceLastBounce);
	if (sinceLastBounce > bounceFrequency) {
		sinceLastBounce -= bounceFrequency;
		GetComponent.<Rigidbody>().AddRelativeForce(Vector3.up * bounceForce);
	}
}
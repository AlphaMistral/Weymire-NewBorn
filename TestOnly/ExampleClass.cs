using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	private float duration = 10f;
	private float magnitude = 0.01f;
	private Vector3 target = new Vector3(360f, 360f, 0f);
	private Vector3 shouldBePosition = new Vector3 (0f, 0f, 0f);

	double tot = 0f;
	int sum = 0;
	int sum1 = 0;
	void Update() {
		return;
		float height = Mathf.PerlinNoise(Time.time * 10f, 0.0F);
		float width = (Mathf.PerlinNoise(0.0f, Time.time * 10f));
		Vector3 pos = transform.position;
		pos.y = (height - 0.4655f) * 80f;
		pos.x = (width - 0.4655f) * 80f;
		sum++;
		tot += height;
		Debug.Log(tot / sum);
		transform.localPosition = pos;
	}
	void Start ()
	{
		StartCoroutine(TestShake());
	}
	IEnumerator Shake() {

		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position += new Vector3(x, y, 0f);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}

	IEnumerator TestShake ()
	{
		yield return new WaitForSeconds(3f);
		while (Vector3.Distance(shouldBePosition, target) > 1f)
		{
			shouldBePosition = Vector3.Slerp(shouldBePosition, target, Time.deltaTime/5);
			float height = Mathf.PerlinNoise(Time.time * 10f, 0.0F);
			float width = (Mathf.PerlinNoise(0.0f, Time.time * 10f));
			Vector3 pos = shouldBePosition;
			pos.y += (height - 0.4655f) * 30f;
			pos.x += (width - 0.4655f) * 30f;
			transform.localPosition = pos;
			Debug.Log("!11");
			yield return new WaitForSeconds(Time.deltaTime);
		}
		Debug.Log("Over");
		shouldBePosition = target;
		transform.localPosition = target;
	}
}
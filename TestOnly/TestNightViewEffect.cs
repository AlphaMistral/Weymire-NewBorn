using UnityEngine;
using System.Collections;

public class TestNightViewEffect : MonoBehaviour 
{
	public Shader shader;

	public Texture2D vignetteTexture;
	public Texture2D scanLineTexture;
	public Texture2D noiseTexture;

	public float contrast = 2.0f;
	public float brightness = 1.0f;
	public float scanLineTileAmount = 4.0f;
	public float noiseXSpeed = 2.0f;
	public float noiseYSpeed = 2.0f;
	public float distortion = 0.2f;
	public float scale = 0.8f;
	public float randomeValue = 0.0f;

	public Color nightVisionColor = Color.white;
	private Material curMaterial;

	private Material material
	{
		get
		{
			if (curMaterial == null)
			{
				curMaterial = new Material(shader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}

	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (shader != null)
		{
			material.SetFloat("_Contrast", contrast);
			material.SetFloat("_Brightness", brightness);
			material.SetColor("_NightVisionColor", nightVisionColor);
			material.SetFloat("_RandomValue", randomeValue);
			material.SetFloat("_distortion", distortion);
			material.SetFloat("_scale", scale);
			material.SetFloat("_ScanLineTileAmount", scanLineTileAmount);

			if (vignetteTexture)
				material.SetTexture("_VignetteTex", vignetteTexture);
			if (scanLineTexture)
				material.SetTexture("_ScanLineTex", scanLineTexture);
			if (noiseTexture)
			{
				material.SetTexture("_NoiseTex", noiseTexture);
				material.SetFloat("_NoiseXSpeed", noiseXSpeed);
				material.SetFloat("_NoiseYSpeed", noiseYSpeed);
			}
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	void Update ()
	{
		contrast = Mathf.Clamp(contrast, 0f, 4f);
		brightness = Mathf.Clamp(brightness, 0f, 2f);
		randomeValue = Random.Range(-1f, 1f);
		distortion = Mathf.Clamp(distortion, -1f, 1f);
		scale = Mathf.Clamp(scale, 0f, 3f);
	}
}

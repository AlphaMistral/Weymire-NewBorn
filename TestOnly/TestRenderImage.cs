using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TestRenderImage : MonoBehaviour 
{
	public Shader ourShader;
	public float grayScaleAmount = 1.0f;
	private Material curMaterial;

	private Material material
	{
		get
		{
			if (curMaterial == null)
			{
				curMaterial = new Material(ourShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}

	void Start ()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		if (!ourShader && !ourShader.isSupported)
			enabled = false;
	}

	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (ourShader != null)
		{
			material.SetFloat("_LuminosityAmount", grayScaleAmount);
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
			Graphics.Blit(sourceTexture, destTexture);
	}

	void Update ()
	{
		grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);
	}

	void OnDisable()
	{
		if (curMaterial)
			DestroyImmediate(curMaterial);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureScript : MonoBehaviour
{

	public RenderTexture renderTextureL;
	public RenderTexture renderTextureR;

	Camera _camera;
	RenderTexture _cubemap;

	// Use this for initialization
	void Start()
	{
		_camera = GetComponent<Camera>();
		_cubemap = new RenderTexture(1024, 1024, 24, RenderTextureFormat.ARGB32);
		_cubemap.dimension = UnityEngine.Rendering.TextureDimension.Cube;

		//_camera.transform.Translate(-2.5f, 0.0f, 0.0f);
		_camera.stereoSeparation = 5.0f;

		RenderTexture.active = renderTextureL;
		GL.Begin(GL.TRIANGLES);
		GL.Clear(true, true, new Color(1.0f, 0.5f, 0.0f, 1.0f));
		GL.End();

		RenderTexture.active = renderTextureR;
		GL.Begin(GL.TRIANGLES);
		GL.Clear(true, true, new Color(0.5f, 1.0f, 0.0f, 1.0f));
		GL.End();
	}

	// Update is called once per frame
	void Update()
	{
		_camera.RenderToCubemap(_cubemap, 63, Camera.MonoOrStereoscopicEye.Left);
		_cubemap.ConvertToEquirect(renderTextureL, Camera.MonoOrStereoscopicEye.Left);
		_camera.RenderToCubemap(_cubemap, 63, Camera.MonoOrStereoscopicEye.Right);
		_cubemap.ConvertToEquirect(renderTextureR, Camera.MonoOrStereoscopicEye.Right);
	}

	private void OnDestroy()
	{
		if (_cubemap != null)
		{
			Destroy(_cubemap);
			_cubemap = null;
		}
	}
}

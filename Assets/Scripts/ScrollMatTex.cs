using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMatTex : MonoBehaviour
{
	public GameObject quadGameObject;
	private Renderer quadRenderer;

	float scrollSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = quadGameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time*scrollSpeed,0);
		quadRenderer.material.mainTextureOffset = textureOffset;
    }
}

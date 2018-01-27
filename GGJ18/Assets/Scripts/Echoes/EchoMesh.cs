using UnityEngine;

public class EchoMesh : MonoBehaviour {

    public float duration;
    public float startToFadeTime;

    private float lifetime;
    private MeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
        lifetime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        lifetime += Time.deltaTime;
        if (lifetime > startToFadeTime)
        {
            var newAlpha = meshRenderer.material.color.a - (Time.deltaTime / (duration - startToFadeTime));
            if (newAlpha < 0)
            {
                Destroy(gameObject);
            }
            var oldColor = meshRenderer.material.color;
            meshRenderer.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
        }
	}
}

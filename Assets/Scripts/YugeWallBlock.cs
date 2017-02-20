using UnityEngine;

public class YugeWallBlock : MonoBehaviour
{
    [HideInInspector]
    public int x, y;
	public float chroma = 0.01f;
	public float value = 0.1f;


    void Start()
    {
        float intensity = 1f + Random.Range(-value, value);

        var mat = gameObject.GetComponentInChildren<MeshRenderer>().material;
        var col = mat.color;
        mat.color = new Color(col.r * intensity + Random.Range(+chroma, -chroma),
                              col.g * intensity + Random.Range(+chroma, -chroma),
                              col.b * intensity + Random.Range(+chroma, -chroma)
        );
    }

}

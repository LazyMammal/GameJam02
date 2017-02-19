using UnityEngine;

public class SpawnItems : MonoBehaviour , CommandInterface
{
    float maxZ = 10f;
	public float targetX = 5f;
	public float minCount = 1;
	public float maxCount = 10;
    public GameObject[] prefabs;
	public bool nestItem = false;

    public void DoCommand()
    {
        foreach (var item in prefabs)
        {
			for (int i = (int)Random.Range(minCount,maxCount); i >= 0; i--) {
                Quaternion rot = transform.rotation;
				Vector3 pos3 = new Vector3(targetX, 0, Random.Range(-maxZ, maxZ));
				GameObject go = (GameObject)Instantiate(item, transform.position + pos3, transform.rotation);
                if (nestItem)
                {
                    go.transform.SetParent(transform);
                }
            }
        }
    }
}

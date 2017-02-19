using UnityEngine;

public class SpawnItems : MonoBehaviour , CommandInterface
{
    float maxZ = 30f;
	public float targetX = 5f;
	public float minCount = 1;
	public float maxCount = 10;
    public GameObject[] prefabs;
	public bool nestItem = false;

    public void DoCommand()
    {
		Quaternion rot = transform.rotation;
        foreach (var item in prefabs)
        {
			int numItems = Mathf.RoundToInt( Random.Range (minCount, maxCount) );
			for (int i = 0; i < numItems; i += 1) {
				Vector3 pos3 = new Vector3(targetX, 0.5f, i * maxZ / numItems - maxZ/2f );
				GameObject go = (GameObject)Instantiate(item, transform.position + pos3, transform.rotation);
                if (nestItem)
                {
                    go.transform.SetParent(transform);
                }
            }
        }
    }
}

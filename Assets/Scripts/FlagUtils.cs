using System.Collections.Generic;
using UnityEngine;

public class FlagUtils : ScriptableObject
{
	private const string FlagTag = "CastleFlag";
    public static List<GameObject> FindAllFlags()
	{
		return new List<GameObject>(GameObject.FindGameObjectsWithTag(FlagTag));
	}
    public static List<GameObject> FindAllEnemyFlags(Transform parent)
	{
		List<GameObject> flags = new List<GameObject>();
		List<GameObject> allFlags = FindAllFlags();
		foreach( var f in allFlags)
		{
			if (f.transform.parent != parent)
				flags.Add(f);
		}
		return flags;
	}
}

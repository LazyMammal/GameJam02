using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleSpawnUtilities : MonoBehaviour {


	public Material NewFlagMaterial;
	public Material NewLauncherMaterial;

	// Use this for initialization
	void Start () {
		Debug.Log ("This call should be used by the base constructor");
		Invoke ("ChangeFlagColorExample", 1);		
	}

	void ChangeFlagColorExample() {
		ChangeMaterial (NewFlagMaterial, "CastleFlag");
		ChangeMaterial (NewLauncherMaterial, "Launcher");
	}

	public void ChangeMaterial(Material material, string tagName) {
		foreach (Transform childTransform in transform) {
			GameObject child = childTransform.gameObject;
			if (child.CompareTag (tagName)) {
				foreach (Renderer renderer in child.GetComponentsInChildren<Renderer>()) {
					renderer.material = material;
				}
			}
		}
	}		
	
}

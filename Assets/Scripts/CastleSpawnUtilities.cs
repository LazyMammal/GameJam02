using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleSpawnUtilities : MonoBehaviour {


	public Material NewFlagMaterial;
	public Material NewLauncherMaterial;


	public void Spawn() {
		foreach (CommandInterface script in gameObject.GetComponents<CommandInterface>()) {
			script.DoCommand ();
		}

		ChangeMaterial (NewFlagMaterial, "CastleFlag");
		ChangeMaterial (NewLauncherMaterial, "Launcher");
	}

	public void Despawn() {
		foreach (Transform childTransform in transform) {
			Destroy (childTransform.gameObject);
		}
	}


	void ChangeMaterial(Material material, string tagName) {
		foreach (Transform childTransform in transform) {
			GameObject child = childTransform.gameObject;
			if (child.CompareTag (tagName)) {
				foreach (Renderer renderer in child.GetComponentsInChildren<Renderer>()) {
					renderer.material = material;
				}
			}
		}
	}

	public int FlagCount() {
		int count = 0;
		foreach (Transform childTransform in transform) {
			GameObject child = childTransform.gameObject;
			if (child.CompareTag ("CastleFlag")) {
				count++;
			}
		}
		return count;
	}
	
}

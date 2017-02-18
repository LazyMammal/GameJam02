using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCastleScript : MonoBehaviour {

	public void OnClick() {
		Debug.Log("Clicked the onclick");
		SequenceOfPlay.singleton.SwapCastles();
	}

}

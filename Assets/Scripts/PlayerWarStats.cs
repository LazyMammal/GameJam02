using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarStats {

	// what castle do i use?
	public GameObject castleSpawnPoint;

	// what panel do i update?
	public GameObject PlayerPanel;

	// how many flags are alive?
	int flags_remaining = 0;

	// how many flags have i lost?
	int flags_lost = 0;

	public PlayerWarStats( GameObject panel, GameObject castle, int initial_flags ) {
		PlayerPanel = panel;
		castleSpawnPoint = castle;
		flags_remaining = initial_flags;
		flags_lost = 0;
	}

	public void FlagDestroyed( ) {
		flags_remaining--;
		flags_lost++;
	}

	public int FlagRemaining() {
		return flags_remaining;
	}

	public int FlagsLost() {
		return flags_lost;
	}

}

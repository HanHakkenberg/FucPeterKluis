using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	public Animator myAnimator;

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			myAnimator.SetInteger("l", myAnimator.GetInteger("l")+ 1);
			if (myAnimator.GetInteger("l") >= 3) {
				Destroy(this);
			}
		}
	}
}
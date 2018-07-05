using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	public Animator myAnimator;
	public GameObject firstArrow;
	public GameObject SecondArrow;

	void Update() {
		if (Input.GetButton("Inventory")&& myAnimator.GetInteger("l")< 0) {
			myAnimator.SetInteger("l", myAnimator.GetInteger("l")+ 1);
		}

		if (Input.GetMouseButtonDown(0) && myAnimator.GetInteger("l")>= 0) {
			myAnimator.SetInteger("l", myAnimator.GetInteger("l")+ 1);
			if (myAnimator.GetInteger("l")>= 3) {
				firstArrow.SetActive(true);
				SecondArrow.SetActive(true);
				Destroy(this);
			}
		}
	}
}
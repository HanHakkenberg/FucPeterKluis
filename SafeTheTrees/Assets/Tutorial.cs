using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	public Animator myAnimator;
	public GameObject firstArrow;
	public GameObject SecondArrow;
	public TextMeshProUGUI myText;
	public GameObject obj;

	void Update() {
		if (myAnimator.GetInteger("l")< 0) {
			if (Input.GetButton("Inventory")) {
				myAnimator.SetInteger("l", myAnimator.GetInteger("l")+ 1);
			} else {
				myText.text = "Pick up the items and open the inventory";
			}
		} else {
			myText.text = "Craft a pickaxe and cut down the tree";
		}

		if (Input.GetMouseButtonDown(0)&& myAnimator.GetInteger("l")>= 0) {
			myAnimator.SetInteger("l", myAnimator.GetInteger("l")+ 1);
			if (myAnimator.GetInteger("l")>= 4) {
				firstArrow.SetActive(true);
				Destroy(obj);
				SecondArrow.SetActive(true);
				Destroy(this);
			}
		}
	}
}
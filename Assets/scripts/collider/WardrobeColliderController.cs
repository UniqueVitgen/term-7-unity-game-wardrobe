using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeColliderController : MonoBehaviour {
	public GameObject Player;
	public GameObject WardrobeAttendantPerson;
	public GameObject AttendantHand;
	public Animator WardrobeAnimator;
	//public GameObject HangPosition;
	public GameObject GiveToWardrobeText;
	public GameObject TakeFromWardrobeText;
	public GameObject Thing;
	public GameObject TakePosition;
	public GameObject HangPosition;
	public GameObject HangAttendantPosition;
	public float speed;
	public GameObject CrutchJacket;
	public GameObject TrueJacket;
	public GameObject FalseJacket;
	private AttendantStateEnum AttendantState;
	private ThingStateEnum ThingState;
	private float secondForWait;
	Animator animator;
	private StudentStateEnum StudentState;

	// Use this for initialization
	void Start () {
		secondForWait = 1.0f;
		setAttendantState(AttendantStateEnum.Waiting);
		setThignState (ThingStateEnum.InOwner);
		GiveToWardrobeText.SetActive (false);
		TakeFromWardrobeText.SetActive (false);
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		RenderThingState ();
		RenderAttendantState ();
	}

	private void RenderThingState() {
		if (ThingState == ThingStateEnum.InHanging) {
			if (Thing.transform.parent != HangPosition.transform) {
				ChangeParent (Thing, HangPosition);
			}
		} else if (ThingState == ThingStateEnum.InAttendant) {
			if (!this.CrutchJacket.activeSelf) {
				this.FalseJacket.SetActive (true);
				this.CrutchJacket.SetActive (true);
				this.TrueJacket.SetActive (false);
			}
			if (Thing.transform.parent != AttendantHand.transform) {
				ChangeParent (Thing, AttendantHand);
			}
		} else if (ThingState == ThingStateEnum.InOwner) {
			if (Thing.transform.parent != Player.transform) {
				ChangeParent (Thing, Player);
			}
			if (this.CrutchJacket.activeSelf) {
				this.FalseJacket.SetActive (false);
				this.CrutchJacket.SetActive (false);
				this.TrueJacket.SetActive (true);
			}
		}
	}

	private void RenderAttendantState() {
		if (AttendantState == AttendantStateEnum.WalkingToHangingPlace) {
			attendantGoToHangPosition ();
		} else if (AttendantState == AttendantStateEnum.WalkingToTakingPlace) {
			attendantGoToTakePosition ();
		}
		else if (AttendantState == AttendantStateEnum.Waiting) {
			WardrobeAttendantPerson.transform.position = TakePosition.transform.position;
		}
	}

	private void setPlayerState(StudentStateEnum state) {
		if (state == StudentStateEnum.Stay) {
			animator.SetInteger ("state", 0);
		} else if (state == StudentStateEnum.Run) {
			animator.SetInteger ("state", 1);
		} else if (state == StudentStateEnum.TakeFrom) {
			animator.SetInteger ("state", 2);
		} else if (state == StudentStateEnum.TakeTo) {
			animator.SetInteger ("state", 2);
		} else if (state == StudentStateEnum.TakingProcessDown) {
			animator.SetInteger ("state", 4);
		} else if (state == StudentStateEnum.TakingProcessStay) {
			animator.SetInteger ("state", 3);
		}
		StudentState = state;
		print ("StudentState: " + state);

	}

	private  void setAttendantState(AttendantStateEnum state) {
		if (state == AttendantStateEnum.HangingOut) {
			WardrobeAnimator.SetInteger ("state", 3);
		} else if (state == AttendantStateEnum.HanginIn) {
			WardrobeAnimator.SetInteger ("state", 3);
		} else if (state == AttendantStateEnum.TakingIn) {
			WardrobeAnimator.SetInteger ("state", 3);
		} else if (state == AttendantStateEnum.TakingOut) {
			WardrobeAnimator.SetInteger ("state", 3);
		} else if (state == AttendantStateEnum.Waiting) {
			WardrobeAnimator.SetInteger ("state", 0);
		} else if (state == AttendantStateEnum.WalkingToHangingPlace) {
			WardrobeAnimator.SetInteger ("state", 1);
		} else if (state == AttendantStateEnum.WalkingToTakingPlace) {
			WardrobeAnimator.SetInteger ("state", 1);
		} else if (state == AttendantStateEnum.TakingProcessStay) {
			WardrobeAnimator.SetInteger ("state", 4);
		} else if (state == AttendantStateEnum.TakingProcessDown) {
			WardrobeAnimator.SetInteger ("state", 5);
		}
		AttendantState = state;
		//print ("AttendantState " + state);
	}

	private void setThignState(ThingStateEnum state) {
		ThingState = state;
	}

	private void attendantGoToTakePosition() {
		float step = speed * Time.deltaTime;
		GameObjectMoveToAnotherObject (WardrobeAttendantPerson, TakePosition, step);
		//WardrobeAttendantPerson.transform.position = Vector3.MoveTowards(WardrobeAttendantPerson.transform.position, TakePosition.transform.position, step);
		//WardrobeAttendantPerson.transform.rotation = Quaternion.LookRotation(TakePosition.transform.position);
		float dist = Vector3.Distance(WardrobeAttendantPerson.transform.position, TakePosition.transform.position);
		if (dist < step) {
			if (ThingState == ThingStateEnum.InHanging) {
				setAttendantState (AttendantStateEnum.Waiting);
			} else if (ThingState == ThingStateEnum.InAttendant) {
				StartCoroutine(TakingFromWardrobe());
			}
			//setAttendantState (AttendantStateEnum.TakingOut);
			//setThignState (ThingStateEnum.InTaking);
		}
	}

	private IEnumerator TakingFromWardrobe() {
		setThignState (ThingStateEnum.InTaking);
		setAttendantState (AttendantStateEnum.TakingOut);
		setPlayerState (StudentStateEnum.TakeFrom);
		yield return new WaitForSeconds(secondForWait);
		setPlayerState (StudentStateEnum.TakingProcessStay);
		setAttendantState (AttendantStateEnum.TakingProcessDown);

		yield return new WaitForSeconds (secondForWait);
		setPlayerState (StudentStateEnum.TakingProcessDown);
		setThignState (ThingStateEnum.InOwner);
		setAttendantState (AttendantStateEnum.Waiting);
	}

	private void attendantGoToHangPosition() {
		float step = speed * Time.deltaTime;
		GameObjectMoveToAnotherObject (WardrobeAttendantPerson, HangAttendantPosition, step);
		
		float dist = Vector3.Distance(WardrobeAttendantPerson.transform.position, HangAttendantPosition.transform.position);
		if (dist < step) {
			if (ThingState == ThingStateEnum.InHanging) {
				StartCoroutine (HangFromHangPosition ());
			} else if (ThingState == ThingStateEnum.InAttendant) {
				StartCoroutine (HangToHangPosition ());
			}
		}
	}

	private IEnumerator HangToHangPosition() {
		setThignState (ThingStateEnum.InTaking);
		setAttendantState (AttendantStateEnum.HanginIn);
		yield return new WaitForSeconds(secondForWait);
		setAttendantState (AttendantStateEnum.TakingProcessDown);

		yield return new WaitForSeconds (secondForWait);
		setAttendantState (AttendantStateEnum.WalkingToTakingPlace);
		setThignState (ThingStateEnum.InHanging);
	}

	private IEnumerator HangFromHangPosition() {
		setThignState (ThingStateEnum.InTaking);
		setAttendantState (AttendantStateEnum.HangingOut);
		yield return new WaitForSeconds(secondForWait);
		setAttendantState (AttendantStateEnum.TakingProcessDown);

		yield return new WaitForSeconds (secondForWait);
		setAttendantState (AttendantStateEnum.WalkingToTakingPlace);
		setThignState (ThingStateEnum.InAttendant);
	}

	private void GiveThing() {
		StartCoroutine (TakingToWardrobe ());
	}

	private IEnumerator TakingToWardrobe() {
		setThignState (ThingStateEnum.InTaking);
		setAttendantState (AttendantStateEnum.TakingIn);
		setPlayerState (StudentStateEnum.TakeTo);
		yield return new WaitForSeconds(secondForWait);
		setPlayerState (StudentStateEnum.TakingProcessStay);
		setAttendantState (AttendantStateEnum.TakingProcessDown);

		yield return new WaitForSeconds (secondForWait);
		setPlayerState (StudentStateEnum.TakingProcessDown);
		setThignState (ThingStateEnum.InAttendant);
		setAttendantState (AttendantStateEnum.WalkingToHangingPlace);
	}

	private void TakeThing() {
		setThignState (ThingStateEnum.InHanging);
		setAttendantState (AttendantStateEnum.WalkingToHangingPlace);
	}

	void OnTriggerStay(Collider col) {
		if (col.name == "WardrobeTrigger") {
			OnWardrobeTriggerStay ();
		}
	}

	private void OnWardrobeTriggerStay() {
		if (AttendantState == AttendantStateEnum.Waiting) {
			if (ThingState == ThingStateEnum.InHanging) {
				GiveToWardrobeText.SetActive (false);
				TakeFromWardrobeText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) {
					TakeThing ();
				}
			} else {
				GiveToWardrobeText.SetActive (true);
				TakeFromWardrobeText.SetActive (false);
				if (Input.GetKeyDown (KeyCode.E)) {
					GiveThing ();
				}
			}
		} else {
			GiveToWardrobeText.SetActive (false);
			TakeFromWardrobeText.SetActive (false);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.name == "WardrobeTrigger") {
			TakeFromWardrobeText.SetActive (false);
			GiveToWardrobeText.SetActive (false);
		}
	}

	private void ChangeParent(GameObject Child, GameObject NewParent) {
		Child.transform.parent = NewParent.transform;
		Child.transform.position = NewParent.transform.position;
	}

	private void GameObjectMoveToAnotherObject(GameObject source, GameObject target, float step) {
		source.transform.position = Vector3.MoveTowards(source.transform.position, target.transform.position, step);
		GameObjectRotationToTargetObject (source, target, step);
	}

	private void GameObjectRotationToTargetObject(GameObject source, GameObject target, float step) {

		//v2
		Vector3 targetDir = target.transform.position - source.transform.position;

		Vector3 newDir = Vector3.RotateTowards(source.transform.forward, targetDir, 20.0f, 0.0f);
		Debug.DrawRay(transform.position, newDir, Color.red);

		source.transform.rotation = Quaternion.LookRotation(newDir);

		//v3
		
		//Vector3 _direction = (target.transform.position - source.transform.position).normalized;
		//Quaternion _lookRotation = Quaternion.LookRotation(_direction);

		//source.transform.rotation = Quaternion.Slerp(source.transform.rotation, _lookRotation, 20.0f);
	}
}

enum AttendantStateEnum {
	Waiting, TakingIn, TakingOut, HanginIn, HangingOut, WalkingToHangingPlace, 
	WalkingToTakingPlace, TakingProcessStay, TakingProcessDown
}

enum ThingStateEnum {
	InOwner, InAttendant, InHanging, InTaking
}

enum StudentStateEnum {
	Run, TakeTo, TakeFrom,  TakingProcessStay, TakingProcessDown, Stay
}

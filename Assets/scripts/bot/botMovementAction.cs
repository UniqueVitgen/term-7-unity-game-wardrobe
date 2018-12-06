using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botMovementAction : MonoBehaviour {
	public GameObject Bot;
	public float SecondsForWait;
	public float speed;
	private BotStateEnum BotState;
	public GameObject[] BotPoints;
	private GameObject TargetBotPoint;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = Bot.GetComponent<Animator> ();
		SetBotState (BotStateEnum.Wait);
		//BotPoints = GameObject.FindGameObjectsWithTag ("BotPoint");
		StartCoroutine (Action ());
	}

	// Update is called once per frame
	void Update () {
		this.RenderBot ();
	}

	private void GameObjectMoveToAnotherObject(GameObject source, GameObject target, float step) {
		if (target != null) {
			source.transform.position = Vector3.MoveTowards(source.transform.position, target.transform.position, step);
			GameObjectRotationToTargetObject (source, target, step);
		}
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

	private void SetBotState(BotStateEnum BotState) {
		if (BotState == BotStateEnum.Wait) {
			animator.SetInteger ("state", 5);
		} else if (BotState == BotStateEnum.Go
		) {
			animator.SetInteger ("state", 1);
		} 
		//else if (state == BotStateEnum.TakeFrom) {
		//	animator.SetInteger ("state", 2);
		//} else if (state == BotStateEnum.TakeTo) {
		//	animator.SetInteger ("state", 2);
		//} else if (state == BotStateEnum.TakingProcessDown) {
		//	animator.SetInteger ("state", 4);
		//} else if (state == BotStateEnum.TakingProcessStay) {
		//	animator.SetInteger ("state", 3);
		//}
		this.BotState = BotState;
	}

	private IEnumerator Action() {
		while (true) {
			//this.Bot.SetActive (false);
			//Bot.enabled = false;
			yield return new WaitForSeconds(SecondsForWait);
			//Bot.enabled = true;
			this.Bot.SetActive (true);
			foreach(GameObject BotPoint in BotPoints) {
				//if (BotPoint.name == "StartPoint") {
					//this.Bot.SetActive (true);
				//}
		
				if (BotPoint.name == "WardrobePointGive" || BotPoint.name == "BuffetPoint" ) {
					//this.Bot.SetActive (false);
					yield return new WaitForSeconds (SecondsForWait);
					//this.Bot.SetActive (true);
				} else {
					yield return new WaitForSeconds (SecondsForWait);
				}
				this.TargetBotPoint = BotPoint;
				this.SetBotState (BotStateEnum.Go);
				//if (BotPoint.name == "StairsPoint" || BotPoint.name == "StartPoint" ) {

				//} else {
					//yield return new WaitForSeconds(SecondsForWait);
				//}


			}
		}
	}

	private void RenderBot() {
		if (this.TargetBotPoint != null) {
			if (this.Bot.activeSelf) {
				float step = speed * Time.deltaTime;
				if (this.BotState == BotStateEnum.Go) {
					GameObjectMoveToAnotherObject (this.Bot, this.TargetBotPoint, step);
				}

				float dist = Vector3.Distance (this.Bot.transform.position, this.TargetBotPoint.transform.position);
				if (dist < step) {
					if ((TargetBotPoint.name == "StartPoint" || TargetBotPoint.name == "StairsPoint")) {
						if (this.Bot.activeSelf) {
							Bot.SetActive (false);
						}
					}
					SetBotState (BotStateEnum.Wait);
				}
			} else {
				if (!(TargetBotPoint.name == "StartPoint" || TargetBotPoint.name == "StairsPoint")) {
					Bot.SetActive (true);
				}
			}
		}
	}
}

enum BotStateEnum {
	Wait,
	Go,
}

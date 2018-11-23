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
		animator = GetComponent<Animator> ();
		SetBotState (BotStateEnum.Go);
		//BotPoints = GameObject.FindGameObjectsWithTag ("BotPoint");
		StartCoroutine (Action ());
	}

	// Update is called once per frame
	void Update () {
		this.RenderBot ();
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
			foreach(GameObject BotPoint in BotPoints) {
				this.TargetBotPoint = BotPoint;
				this.SetBotState (BotStateEnum.Go);
				yield return new WaitForSeconds(SecondsForWait);
			}
		}
	}

	private void RenderBot() {
		float step = speed * Time.deltaTime;
		if (this.BotState == BotStateEnum.Go) {
			GameObjectMoveToAnotherObject(this.Bot, this.TargetBotPoint, step);
		}

		float dist = Vector3.Distance(this.Bot.transform.position, this.TargetBotPoint.transform.position);
		if (dist < step) {
			SetBotState (BotStateEnum.Wait);
		}
	}
}

enum BotStateEnum {
	Wait,
	Go,
}

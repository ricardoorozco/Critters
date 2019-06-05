using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetMesh : MonoBehaviour {

	[SerializeField] private GoTargetNavMesh goTarget;
	[SerializeField] private Transform[] targets;
	[SerializeField] private bool enable = false;

	private void OnMouseDown()
	{
		if (enable) {
			if (targets != null && goTarget != null) {
				goTarget.SetTargets (targets);
			}
		}
	}

	public void setEnableStatus(bool enable)
	{
		this.enable = enable;
	}
}

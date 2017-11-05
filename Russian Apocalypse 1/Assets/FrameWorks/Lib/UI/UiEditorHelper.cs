﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEditorHelper : MonoBehaviour {

	[ContextMenu("Alling Anchor")]
	void AlingTheAnchor()
	{
		RectTransform rt = GetComponent<RectTransform>();
		V.UIHelper.MatchCornersToAnchors(ref rt);
	}
}

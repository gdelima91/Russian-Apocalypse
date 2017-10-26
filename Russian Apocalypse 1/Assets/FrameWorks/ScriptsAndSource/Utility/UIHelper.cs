using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vinsin1_1
{
	public class UIHelper
	{

		/// <summary>
		///     Set the four corner of the rectTransform to it's four Anchors
		/// when we set offsetMax and offsetMin to Vector2.zero; the corner and the size
		/// of the UI element will be fully match to its Anchors
		/// </summary>
		/// <param name="rectTransform"></param>
		public static void MatchCornersToAnchors(ref RectTransform rectTransform)
		{
			rectTransform.offsetMax = Vector2.zero;
			rectTransform.offsetMin = Vector2.zero;
		}

		/// <summary>
		/// Move the RectTransform...
		/// </summary>
		/// <param name="rectTransform"> the rectTransform we wanna to move</param>
		/// <param name="move">the value of move should be in the range[0,1]. It's like a the UV which related to rectTransform's parent rectTransform</param>
		public static void MoveAnchors(ref RectTransform rectTransform, Vector2 move)
		{
			Vector2 anchorMax = rectTransform.anchorMax;
			Vector2 anchorMin = rectTransform.anchorMin;
			rectTransform.anchorMax += new Vector2(move.x, move.y);
			rectTransform.anchorMin += new Vector2(move.x, move.y);
		}

		public static void SetAnchors(ref RectTransform rectTransform,Vector2 max,Vector2 min)
		{
			rectTransform.anchorMax = max;
			rectTransform.anchorMin = min;
		}
	}

}
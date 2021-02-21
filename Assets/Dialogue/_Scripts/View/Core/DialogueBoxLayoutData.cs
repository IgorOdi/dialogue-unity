using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogues.View {

	[Serializable]
	public class LayoutData {

		public string LayoutName { get; set; }
		public List<Vector2> positionsList = new List<Vector2> ();
		public List<Vector2> scaleList = new List<Vector2> ();
	}

	public class DialogueBoxLayoutData : MonoBehaviour {

		[SerializeField]
		private List<RectTransform> rectTransforms = new List<RectTransform> ();
		[SerializeField]
		internal List<LayoutData> layoutDatas = new List<LayoutData> ();

		private void Reset() {

			rectTransforms = GetComponentsInChildren<RectTransform> ().ToList ();
			AddPosition ();
		}

		private void OnValidate() {

			rectTransforms = GetComponentsInChildren<RectTransform> ().ToList ();
		}

		internal void AddPosition() {

			int index = layoutDatas.Count;
			if (layoutDatas.Count <= index) layoutDatas.Add (new LayoutData ());
			SetPosition (index);
		}

		internal void SetPosition(int index) {

			layoutDatas[index].positionsList = new List<Vector2> ();
			layoutDatas[index].scaleList = new List<Vector2> ();
			for (int i = 0; i < rectTransforms.Count; i++) {

				layoutDatas[index].positionsList.Add (rectTransforms[i].position);
				layoutDatas[index].scaleList.Add (rectTransforms[i].localScale);
			}
		}

		internal void ConfigureSet(int index) {

			for (int i = 0; i < rectTransforms.Count; i++) {

				rectTransforms[i].position = layoutDatas[index].positionsList[i];
				rectTransforms[i].localScale = layoutDatas[index].scaleList[i];
			}
		}

		internal void ConfigureSet(LayoutData layoutData) {

			for (int i = 0; i < rectTransforms.Count; i++) {

				rectTransforms[i].position = layoutData.positionsList[i];
				rectTransforms[i].localScale = layoutData.scaleList[i];
			}
		}
	}
}
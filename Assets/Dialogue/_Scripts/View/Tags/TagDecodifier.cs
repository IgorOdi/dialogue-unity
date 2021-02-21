using System;
using System.Collections.Generic;
using System.Linq;
using Dialogues.Model.Tags;
using UnityEngine;

namespace Dialogues.View.Tags {

	public struct TagInfo {

		public readonly int TagPosition;
		public readonly List<float> Parameters;
		public readonly Action<BaseTextWriter, int, List<float>> OnTag;

		public TagInfo(int tagPosition, List<float> @params, Action<BaseTextWriter, int, List<float>> onTag) {

			TagPosition = tagPosition;
			Parameters = @params;
			OnTag = onTag;
		}
	}

	public static class TagDecodifier {

		public static List<Tag> TagList { get { return TagDatabase.TagList; } }

		public static (List<TagInfo> tagInfos, string newText) Decodify(string text) {

			List<TagInfo> tagInfos = new List<TagInfo> ();

			for (int i = 0; i < text.Length; i++) {

				if (text[i].Equals ('<')) {

					int indexOfCloseTag = 0;
					for (int j = i; j <= text.Length; j++) {
						if (text[j].Equals ('>')) {
							indexOfCloseTag = j + 1;
							break;
						}
					}

					string tagString = text.Substring (i, indexOfCloseTag - i);
					Tag tag = TagDatabase.GetTagWithString (tagString);
					if (tag == null) {

						continue;
					}

					List<float> parameter = tagString.Contains ("=") ? DecodifyParameter (tagString) : null;
					bool isOpening = !tagString[1].Equals ('/');

					text = text.Remove (i, indexOfCloseTag - i);
					tagInfos.Add (new TagInfo (i, parameter, isOpening ? tag.OnOpenTag : tag.OnCloseTag));
				}
			}

			return (tagInfos, text);
		}

		private static List<float> DecodifyParameter(string param) {

			int equalsIndex = param.IndexOf ('=') + 1;
			int endTag = param.IndexOf ('>');
			param = param.Substring (equalsIndex, endTag - equalsIndex);
			int paraAmount = param.Count (s => s.Equals ('/'));

			List<float> paramResult = new List<float> ();
			foreach (string s in param.Split ('/')) {

				float paramValue = 0;
				if (!float.TryParse (s, out paramValue)) {
					throw new Exception ("Parameter format incorrect");
				}
				paramResult.Add (paramValue);
			}

			return paramResult;
		}
	}
}
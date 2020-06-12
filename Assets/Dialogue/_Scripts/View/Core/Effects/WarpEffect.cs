using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//BASED ON WARP EXAMPLE FROM TEXT MESH PRO EXAMPLE
namespace Dialogues.View.Effect {

    public class WarpEffect : TextEffect, ITextEffect {

        [SerializeField]
        private AnimationCurve _vertexCurve = new AnimationCurve (new Keyframe (0, 0), new Keyframe (0.25f, 2.0f),
            new Keyframe (0.5f, 0), new Keyframe (0.75f, 2.0f), new Keyframe (1, 0f));
        private Keyframe _nextKeyFrame;

        private float _curveScale = 1.0f;
        private float _speedMultiplier = 1.0f;

        private const float CURVE_BASE_SCALE = 6f;
        private const float CURVE_BASE_SPEED = 0.0025f;

        protected override void OnEnable () {

            base.OnEnable ();
            _vertexCurve.preWrapMode = WrapMode.Loop;
            _vertexCurve.postWrapMode = WrapMode.Loop;
            _textWriter.RegisterEffect (this);
        }

        protected override void OnDisable () {

            _textWriter.UnregisterEffect (this);
            base.OnDisable ();
        }

        public void RegisterValues (int start, int end) {

            if (_intervalValues.Count > 0 && _intervalValues[_intervalValues.Count - 1].Item2 == 999) {

                _intervalValues[_intervalValues.Count - 1] = (_intervalValues[_intervalValues.Count - 1].Item1, end);
            } else {

                _intervalValues.Add ((start, end));
            }
        }

        public void SetParameters (List<float> parameters) {

            if (parameters == null) return;

            if (parameters.Count > 0) _curveScale = parameters[0];
            if (parameters.Count > 1) _speedMultiplier = parameters[1];
        }

        public void ClearValues () {

            _intervalValues.Clear ();
        }

        private AnimationCurve CopyAnimationCurve (AnimationCurve curve) {

            AnimationCurve newCurve = new AnimationCurve ();
            newCurve.keys = curve.keys;
            return newCurve;
        }

        protected override IEnumerator Effect () {

            Vector3[] vertices;

            _textComponent.havePropertiesChanged = true; // Need to force the TextMeshPro Object to be updated.

            while (true) {

                _textComponent.ForceMeshUpdate (); // Generate the mesh and populate the textInfo with data we can use and manipulate.

                TMP_TextInfo textInfo = _textComponent.textInfo;
                int characterCount = textInfo.characterCount;

                for (int i = 0; i < _vertexCurve.length; i++) {

                    _nextKeyFrame = _vertexCurve.keys[i];
                    _nextKeyFrame.time += CURVE_BASE_SPEED * _speedMultiplier;
                    _vertexCurve.MoveKey (i, _nextKeyFrame);
                }

                if (characterCount == 0) continue;

                float _boundsMinX = _textComponent.bounds.min.x;
                float _boundsMaxX = _textComponent.bounds.max.x;

                for (int i = 0; i < characterCount; i++) {

                    if (!textInfo.characterInfo[i].isVisible)
                        continue;

                    for (int j = 0; j < _intervalValues.Count; j++) {

                        if (i >= _intervalValues[j].Item1 && i < _intervalValues[j].Item2) {

                            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                            vertices = textInfo.meshInfo[0].vertices;

                            for (int k = 0; k < 4; k++) {
                                Vector3 offsetToMidBaseline = new Vector2 ((vertices[vertexIndex + 0].x + vertices[vertexIndex + 2].x) / 2,
                                    textInfo.characterInfo[i].baseLine);
                                float xPoint = (offsetToMidBaseline.x - _boundsMinX) / (_boundsMaxX - _boundsMinX); 

                                vertices[vertexIndex + k] += Vector3.up * _vertexCurve.Evaluate (xPoint) * _curveScale * CURVE_BASE_SCALE;
                            }
                        }
                    }
                }

                _textComponent.UpdateVertexData ();
                yield return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//BASED ON VERTEX JITTER FROM TEXT MESH PRO EXAMPLE
namespace Dialogues.View.Effect {

    public class JitterEffect : TextEffect {

        [Range (0, 1f)]
        private float _speedMultiplier = 0.5f;
        private float _angleMultiplier = 1f;
        private float _curveScale = 4f;

        private struct VertexAnim {

            public float angleRange;
            public float angle;
            public float speed;
        }

        protected override void OnEnable () {

            base.OnEnable ();
            _textWriter.RegisterEffect (this);
        }

        protected override void OnDisable () {

            _textWriter.UnregisterEffect (this);
            base.OnDisable ();
        }

        public override void SetParameters (List<float> parameters) {

            if (parameters.Count > 0) _angleMultiplier = parameters[0];
            if (parameters.Count > 1) _speedMultiplier = parameters[1];
            if (parameters.Count > 2) _curveScale = parameters[2];
        }

        public override void RegisterValues (int start, int end) {

            if (_intervalValues.Count > 0 && _intervalValues[_intervalValues.Count - 1].Item2 == 999) {

                _intervalValues[_intervalValues.Count - 1] = (_intervalValues[_intervalValues.Count - 1].Item1, end);
            } else {

                _intervalValues.Add ((start, end));
            }
        }

        protected override IEnumerator Effect () {

            _textComponent.ForceMeshUpdate ();

            TMP_TextInfo textInfo = _textComponent.textInfo;
            Matrix4x4 matrix;

            int loopCount = 0;
            _hasTextChanged = true;

            VertexAnim[] vertexAnim = new VertexAnim[1024];
            for (int i = 0; i < 1024; i++) {
                vertexAnim[i].angleRange = Random.Range (10f, 25f);
                vertexAnim[i].speed = Random.Range (1f, 3f);
            }

            TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData ();

            while (true) {

                if (_hasTextChanged) {

                    cachedMeshInfo = textInfo.CopyMeshInfoVertexData ();
                    _hasTextChanged = false;
                }

                int characterCount = textInfo.characterCount;

                if (characterCount == 0) {
                    yield return new WaitForSeconds (0.05f);
                    continue;
                }

                for (int i = 0; i < characterCount; i++) {

                    for (int j = 0; j < _intervalValues.Count; j++) {

                        if (i >= _intervalValues[j].Item1 && i <= _intervalValues[j].Item2) {

                            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                            if (!charInfo.isVisible)
                                continue;

                            VertexAnim vertAnim = vertexAnim[i];

                            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                            Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;
                            Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;
                            Vector3 offset = charMidBasline;
                            Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                            vertAnim.angle = Mathf.SmoothStep (-vertAnim.angleRange, vertAnim.angleRange, Mathf.PingPong (loopCount / 25f * vertAnim.speed * _speedMultiplier, 1f));
                            Vector3 jitterOffset = new Vector3 (Random.Range (-.25f, .25f), Random.Range (-.25f, .25f), 0);

                            matrix = Matrix4x4.TRS (jitterOffset * _curveScale, Quaternion.Euler (0, 0, Random.Range (-5f, 5f) * _angleMultiplier), Vector3.one);

                            for (int k = 0; k < 4; k++) {

                                destinationVertices[vertexIndex + k] = sourceVertices[vertexIndex + k] - offset;
                                destinationVertices[vertexIndex + k] = matrix.MultiplyPoint3x4 (destinationVertices[vertexIndex + k]);
                                destinationVertices[vertexIndex + k] += offset;
                            }
                            vertexAnim[i] = vertAnim;
                        }
                    }
                }

                for (int i = 0; i < textInfo.meshInfo.Length; i++) {
                    textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                    _textComponent.UpdateGeometry (textInfo.meshInfo[i].mesh, i);
                }

                loopCount += 1;

                _speedMultiplier = Mathf.Clamp01 (_speedMultiplier);
                yield return new WaitForSeconds (0.1f * (1 - _speedMultiplier));
            }
        }

    }
}
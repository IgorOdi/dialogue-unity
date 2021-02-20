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

            // We force an update of the text object since it would only be updated at the end of the frame. Ie. before this code is executed on the first frame.
            // Alternatively, we could yield and wait until the end of the frame when the text object will be generated.
            _textComponent.ForceMeshUpdate ();

            TMP_TextInfo textInfo = _textComponent.textInfo;

            Matrix4x4 matrix;

            int loopCount = 0;
            _hasTextChanged = true;

            // Create an Array which contains pre-computed Angle Ranges and Speeds for a bunch of characters.
            VertexAnim[] vertexAnim = new VertexAnim[1024];
            for (int i = 0; i < 1024; i++) {
                vertexAnim[i].angleRange = Random.Range (10f, 25f);
                vertexAnim[i].speed = Random.Range (1f, 3f);
            }

            // Cache the vertex data of the text object as the Jitter FX is applied to the original position of the characters.
            TMP_MeshInfo[] cachedMeshInfo = textInfo.CopyMeshInfoVertexData ();

            while (true) {
                // Get new copy of vertex data if the text has changed.
                if (_hasTextChanged) {
                    // Update the copy of the vertex data for the text object.
                    cachedMeshInfo = textInfo.CopyMeshInfoVertexData ();

                    _hasTextChanged = false;
                }

                int characterCount = textInfo.characterCount;

                // If No Characters then just yield and wait for some text to be added
                if (characterCount == 0) {
                    yield return new WaitForSeconds (0.05f);
                    continue;
                }

                for (int i = 0; i < characterCount; i++) {

                    for (int j = 0; j < _intervalValues.Count; j++) {

                        if (i >= _intervalValues[j].Item1 && i <= _intervalValues[j].Item2) {

                            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

                            // Skip characters that are not visible and thus have no geometry to manipulate.
                            if (!charInfo.isVisible)
                                continue;

                            // Retrieve the pre-computed animation data for the given character.
                            VertexAnim vertAnim = vertexAnim[i];

                            // Get the index of the material used by the current character.
                            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                            // Get the index of the first vertex used by this text element.
                            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                            // Get the cached vertices of the mesh used by this text element (character or sprite).
                            Vector3[] sourceVertices = cachedMeshInfo[materialIndex].vertices;

                            // Determine the center point of each character at the baseline.
                            //Vector2 charMidBasline = new Vector2((sourceVertices[vertexIndex + 0].x + sourceVertices[vertexIndex + 2].x) / 2, charInfo.baseLine);
                            // Determine the center point of each character.
                            Vector2 charMidBasline = (sourceVertices[vertexIndex + 0] + sourceVertices[vertexIndex + 2]) / 2;

                            // Need to translate all 4 vertices of each quad to aligned with middle of character / baseline.
                            // This is needed so the matrix TRS is applied at the origin for each character.
                            Vector3 offset = charMidBasline;

                            Vector3[] destinationVertices = textInfo.meshInfo[materialIndex].vertices;

                            destinationVertices[vertexIndex + 0] = sourceVertices[vertexIndex + 0] - offset;
                            destinationVertices[vertexIndex + 1] = sourceVertices[vertexIndex + 1] - offset;
                            destinationVertices[vertexIndex + 2] = sourceVertices[vertexIndex + 2] - offset;
                            destinationVertices[vertexIndex + 3] = sourceVertices[vertexIndex + 3] - offset;

                            vertAnim.angle = Mathf.SmoothStep (-vertAnim.angleRange, vertAnim.angleRange, Mathf.PingPong (loopCount / 25f * vertAnim.speed * _speedMultiplier, 1f));
                            Vector3 jitterOffset = new Vector3 (Random.Range (-.25f, .25f), Random.Range (-.25f, .25f), 0);

                            matrix = Matrix4x4.TRS (jitterOffset * _curveScale, Quaternion.Euler (0, 0, Random.Range (-5f, 5f) * _angleMultiplier), Vector3.one);

                            destinationVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4 (destinationVertices[vertexIndex + 0]);
                            destinationVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4 (destinationVertices[vertexIndex + 1]);
                            destinationVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4 (destinationVertices[vertexIndex + 2]);
                            destinationVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4 (destinationVertices[vertexIndex + 3]);

                            destinationVertices[vertexIndex + 0] += offset;
                            destinationVertices[vertexIndex + 1] += offset;
                            destinationVertices[vertexIndex + 2] += offset;
                            destinationVertices[vertexIndex + 3] += offset;

                            vertexAnim[i] = vertAnim;
                        }
                    }
                }

                // Push changes into meshes
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
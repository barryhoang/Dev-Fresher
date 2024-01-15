#if PRIME_TWEEN_INSTALLED
#if TEXT_MESH_PRO_INSTALLED
using TMPro;
#endif
using JetBrains.Annotations;
using UnityEngine;

namespace PrimeTween.Demo {
    [PublicAPI]
    public class TypewriterAnimatorExample : MonoBehaviour {
        [SerializeField] float charsPerSecond = 40f;
        [SerializeField] int pauseAfterPunctuation = 20;
        #if TEXT_MESH_PRO_INSTALLED
        TextMeshProUGUI text;

        void Awake() {
            text = gameObject.AddComponent<TextMeshProUGUI>();
            text.maxVisibleCharacters = 0;
            text.alignment = TextAlignmentOptions.TopLeft;
            text.fontSize = 12;
            text.color = Color.black * 0.8f;
        }

        public Tween Animate() {
            // or use non-allocating 'text.SetText()' API instead of 'text.text'
            text.text = "This text is <color=orange>animated</color> with <b>zero allocations</b>, see <i>'TypewriterAnimatorExample'</i> script for more details.\n\n" +
                        "PrimeTween rocks!";

            if (pauseAfterPunctuation <= 0) {
                // A simple typewriter animation
                text.ForceMeshUpdate();
                int characterCount = text.textInfo.characterCount;
                return Tween.TextMaxVisibleCharacters(text, 0, characterCount, characterCount / charsPerSecond, Ease.Linear);
            }

            // A more complex typewriter animation which inserts pauses after punctuation marks 
            return TypewriterAnimationWithPunctuations(text, charsPerSecond);
        }

        public Tween TypewriterAnimationWithPunctuations([NotNull] TMP_Text text, float charsPerSecond) {
            text.ForceMeshUpdate();
            RemapWithPunctuations(text, int.MaxValue, out int remappedCount, out _);
            float duration = remappedCount / charsPerSecond;
            return Tween.Custom(this, 0f, remappedCount, duration, (t, x) => t.UpdateMaxVisibleCharsWithPunctuation(x), Ease.Linear);
        }

        void UpdateMaxVisibleCharsWithPunctuation(float progress) {
            int remappedEndIndex = Mathf.RoundToInt(progress);
            RemapWithPunctuations(text, remappedEndIndex, out _, out int visibleCharsCount);
            if (text.maxVisibleCharacters != visibleCharsCount) {
                text.maxVisibleCharacters = visibleCharsCount;
                // todo play keyboard typing sound here if needed
            }
        }
        
        void RemapWithPunctuations([NotNull] TMP_Text text, int remappedEndIndex, out int remappedCount, out int visibleCharsCount) {
            remappedCount = 0;
            visibleCharsCount = 0;
            int count = text.textInfo.characterCount;
            var characterInfos = text.textInfo.characterInfo;
            for (int i = 0; i < count; i++) {
                if (remappedCount >= remappedEndIndex) {
                    break;
                }
                remappedCount++;
                visibleCharsCount++;
                if (IsPunctuationChar(characterInfos[i].character)) {
                    int nextIndex = i + 1;
                    if (nextIndex != count && !IsPunctuationChar(characterInfos[nextIndex].character)) {
                        // add pause after the last subsequent punctuation character
                        remappedCount += pauseAfterPunctuation;
                    }
                }
            }

            static bool IsPunctuationChar(char c) {
                return ".,:;!?".Contains(c);
            }
        }
        #else
        void Awake() {
            Debug.LogWarning("Please install TextMeshPro 'com.unity.textmeshpro' to enable TypewriterAnimatorExample.", this);
        }
        #endif
    }
}
#endif
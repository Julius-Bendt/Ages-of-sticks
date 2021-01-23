using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Juto.UI
{
    public class UIAnimationFramework : MonoBehaviour
    {
        public delegate void EffectOver();



        /// <summary>
        /// Fades a image color
        /// </summary>
        /// <param name="image">The image to animate</param>
        /// <param name="color">color to fade to</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Fade(Image image, Color color, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_Fade(image, color, time, delay, effectOver,curve));
        }

        /// <summary>
        /// Fill aspect animation
        /// </summary>
        /// <param name="image">The image to animate</param>
        /// <param name="value">value to animate to</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Fill(Image image, float value, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_Fill(image, value, time, delay, effectOver, curve));
        }

        /// <summary>
        /// Fades a text color
        /// </summary>
        /// <param name="text">The text to animate</param>
        /// <param name="color">The color to end on</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Fade(TextMeshProUGUI text, Color color, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_TextFade(text, color, time, delay, effectOver, curve));
        }

        /// <summary>
        /// Adds text, based on time.
        /// </summary>
        /// <param name="text">Affected text</param>
        /// <param name="textToAdd">the end text</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine TextAdder(TextMeshProUGUI text, string textToAdd, float time, float delay = 0, EffectOver effectOver = null)
        {

            char[] toadd = textToAdd.ToCharArray();

            return StartCoroutine(_TextAdder(text, toadd, time, delay, effectOver));
        }

        /// <summary>
        /// Smoothly adds a value
        /// </summary>
        /// <param name="text">the text affected</param>
        /// <param name="format">if other text should be included</param>
        /// <param name="from">start value</param>
        /// <param name="to">end value</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine TextValue(TextMeshProUGUI text,string format, int from, int to, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_TextValue(text,format, from, to, time, delay, effectOver, curve));
        }


        /// <summary>
        /// Scales a rect.
        /// </summary>
        /// <param name="transform">Affected RectTransform</param>
        /// <param name="size">size to animate to</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Scale(RectTransform transform, Vector3 size, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_Scale(transform, size, time, delay, effectOver, curve));
        }

        /// <summary>
        /// Moves a RectTransform to point
        /// </summary>
        /// <param name="rectTransform">The affected transform</param>
        /// <param name="point">anchored position to move to</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Move(RectTransform rectTransform, Vector3 point, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_Move(rectTransform, point, time, delay, effectOver, curve));
        }

        /// <summary>
        /// Rotates a RectTransform
        /// </summary>
        /// <param name="rectTransform">The affected transform</param>
        /// <param name="rotateTo">the quaterion to rotate to</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Rotate(RectTransform rectTransform, Quaternion rotateTo, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_Rotate(rectTransform, rotateTo, time, delay, effectOver, curve));
        }


        /// <summary>
        /// Fades a canvas group
        /// </summary>
        /// <param name="cg">The canvas group affected</param>
        /// <param name="visible">0 or 1</param>
        /// <param name="time">Time from start to end</param>
        /// <param name="effectOver">callback</param>
        /// <returns>A Coroutine that controls the animation</returns>
        public Coroutine Fade(CanvasGroup cg, float visible, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            return StartCoroutine(_CanvasGroupAlpha(cg, visible, time,delay, effectOver, curve));
        }

        //Image        
        private IEnumerator _Fade(Image image, Color color, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            Color startingColor = image.color;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {

                if (image == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                image.color = Color.Lerp(startingColor, color, value);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        private IEnumerator _Fill(Image image, float value, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            float startingVal = image.fillAmount;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {

                if (image == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float timeValue = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                image.fillAmount = Mathf.Lerp(startingVal, value, timeValue);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        //Text
        private IEnumerator _TextFade(TextMeshProUGUI text, Color color, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            Color startingColor = text.color;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (text == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                text.color = Color.Lerp(startingColor, color, value);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        private IEnumerator _TextAdder(TextMeshProUGUI text, char[] chars, float time, float delay = 0, EffectOver effectOver = null)
        {
            float TimeForEachChar = time / chars.Length;

            yield return new WaitForSeconds(delay);

            foreach (char c in chars)
            {
                if (text == null)
                    break;

                text.text += c;
                yield return new WaitForSeconds(TimeForEachChar);
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        private IEnumerator _TextValue(TextMeshProUGUI text, string format, int from, int to, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;

            if (format == "")
                format = "{0}";

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (text == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                int currentVal = (int)Mathf.Lerp(from, to, value);
                text.text = string.Format(format, currentVal.ToString()); 
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }


        //Rect
        private IEnumerator _Scale(RectTransform _transform, Vector3 size, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            Vector3 startingScale = _transform.localScale;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (_transform == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                _transform.localScale = Vector3.Lerp(startingScale, size, value);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        private IEnumerator _Move(RectTransform rectTransform, Vector3 point, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            Vector3 startPos = rectTransform.anchoredPosition;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (rectTransform == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                rectTransform.anchoredPosition = Vector3.Lerp(startPos, point, value);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }

        private IEnumerator _Rotate(RectTransform rectTransform, Quaternion rotation, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            Quaternion startRot = rectTransform.rotation;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (rectTransform == null)
                    break;

                ElapsedTime += Time.deltaTime;
                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));
                rectTransform.rotation = Quaternion.Lerp(startRot, rotation, value);
                yield return null;
            }

            if (effectOver != null)
                effectOver.Invoke();
        }


        //Canvas group
        private IEnumerator _CanvasGroupAlpha(CanvasGroup cg, float visible, float time, float delay = 0, EffectOver effectOver = null, AnimationCurve curve = null)
        {
            float ElapsedTime = 0.0f;
            float startingVal = cg.alpha;

            yield return new WaitForSeconds(delay);

            while (ElapsedTime < time)
            {
                if (cg == null)
                    break;

                ElapsedTime += Time.deltaTime;

                float value = (curve == null) ? (ElapsedTime / time) : curve.Evaluate((ElapsedTime / time));

                cg.alpha = Mathf.Lerp(startingVal, visible, value);
                yield return null;
            }

            cg.blocksRaycasts = (visible == 1) ? true:false;
            cg.interactable = (visible == 1) ? true : false;

            if (effectOver != null)
                effectOver.Invoke();

        }
       
    }
}

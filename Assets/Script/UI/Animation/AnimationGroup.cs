using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Juto.UI
{
    [System.Serializable]
    public class AnimationGroup
    {
        public string name;
        public float time, delay;
        public bool isOpen = false;
        private int id;
        public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);

        [Space]

        [Header("Animations")]
        public List<UIAnimation.RectMoveAnimation>    moveAnimations;
        public List<UIAnimation.RectScaleAnimation> scaleAnimations;
        public List<UIAnimation.RectRotateAnimation> rotateAnimation;
        public List<UIAnimation.TextFadeAnimation> textFadeAnimations;
        public List<UIAnimation.ImageFadeAnimation> imageFadeAnimations;
        public List<UIAnimation.ImageFillAnimation> imageFillAnimations;
        public List<UIAnimation.CanvasGroupFadeAnimation> canvasGroupFadeAnimation;

        [Header("Events")]
        public UIAnimation.UIAnimEvent OnStart;
        public UIAnimation.UIAnimEvent OnEnd;

        private UIAnimationFramework effect;

        public void Close(int _id)
        {
            id = _id;
            isOpen = false;
            Animate();
        }

        public void Open(int _id)
        {
            id = _id;
            isOpen = true;
            Animate();
        }

        public void Toggle(int _id)
        {
            id = _id;
            isOpen = !isOpen;
            Animate();

        }

        private void Animate()
        {
            if(effect == null)
                effect = GameObject.FindObjectOfType<UIAnimationFramework>();

            OnStart.Invoke(new UIAnimation.UIAnimationEvent(name, isOpen,id));

            if ((moveAnimations.Count + scaleAnimations.Count + rotateAnimation.Count + textFadeAnimations.Count + imageFadeAnimations.Count + imageFillAnimations.Count + canvasGroupFadeAnimation.Count) == 0)
            {
                OnEnd.Invoke(new UIAnimation.UIAnimationEvent(name, isOpen,id));
                return;
            }

            bool first = true;

            foreach (UIAnimation.RectMoveAnimation move in moveAnimations)
            {
                Vector3 point = (isOpen) ? move.open : move.close;
                float animDelay = (isOpen) ? move.openDelay : move.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Move(move.rect, point, time,delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Move(move.rect, point, time,delay + animDelay,null, curve);
            }

            foreach (UIAnimation.RectScaleAnimation scale in scaleAnimations)
            {
                Vector3 point = (isOpen) ? scale.open : scale.close;
                float animDelay = (isOpen) ? scale.openDelay : scale.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Scale(scale.rect, point, time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Scale(scale.rect, point, time, delay + animDelay, null, curve);
            }

            foreach (UIAnimation.RectRotateAnimation rotate in rotateAnimation)
            {
                Vector3 point = (isOpen) ? rotate.open : rotate.close;
                float animDelay = (isOpen) ? rotate.openDelay : rotate.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Rotate(rotate.rect, Quaternion.Euler(point), time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Rotate(rotate.rect, Quaternion.Euler(point), time, delay + animDelay, null, curve);
            }

            foreach (UIAnimation.TextFadeAnimation fade in textFadeAnimations)
            {
                Color color = (isOpen) ? fade.open : fade.close;
                float animDelay = (isOpen) ? fade.openDelay : fade.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Fade(fade.text, color, time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Fade(fade.text, color, time, delay + animDelay, null, curve);
            }

            foreach (UIAnimation.ImageFadeAnimation image in imageFadeAnimations)
            {
                Color color = (isOpen) ? image.open : image.close;
                float animDelay = (isOpen) ? image.openDelay : image.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Fade(image.image, color, time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Fade(image.image, color, time, delay + animDelay, null, curve);
            }

            foreach (UIAnimation.ImageFillAnimation img in imageFillAnimations)
            {
                float value = (isOpen) ? img.open : img.close;
                float animDelay = (isOpen) ? img.openDelay : img.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Fill(img.image, value, time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Fill(img.image, value, time, delay + animDelay, null, curve);
            }

            foreach (UIAnimation.CanvasGroupFadeAnimation cg in canvasGroupFadeAnimation)
            {
                float visible = (isOpen) ? cg.open : cg.close;
                float animDelay = (isOpen) ? cg.openDelay : cg.closeDelay;

                if (first)
                {
                    //Only fire OnOver event one time.
                    effect.Fade(cg.cg, visible, time, delay + animDelay, OnOver,curve);
                    first = false;
                }
                else
                    effect.Fade(cg.cg, visible, time, delay + animDelay, null, curve);
            }

        }

        public void OnOver()
        {
            OnEnd.Invoke(new UIAnimation.UIAnimationEvent(name,isOpen,id));
        }
    }
}


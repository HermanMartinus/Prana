using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour {

    public AnimationCurve _startAnimationCurve;
    public AnimationCurve _animationCurve;
    public float _scaleUp = 1.3f;
    public float _breathDuration = 3f;
    public Text _startText;
    bool started = false;

    // Use this for initialization
    void Start () {

	}

    public void StartBreathing()
    {
        StopAllCoroutines();
        if(!started)
        {
           StartCoroutine(AnimateStart(Vector2.one * 6, Vector2.one, _breathDuration / 2));
            StartCoroutine(AnimateTextFade(_startText.color, new Color(_startText.color.r, _startText.color.g, _startText.color.b, 0), 2f));
        }
        else
        {
            StartCoroutine(AnimateStart(transform.localScale, Vector2.one * 6, _breathDuration / 2));
            StartCoroutine(AnimateTextFade(_startText.color, new Color(_startText.color.r, _startText.color.g, _startText.color.b, 1), 2f));
        }
        started = !started;
    }

    IEnumerator AnimateBreath(Vector2 origin, Vector2 target, float duration)
    {
        SoundManager.Instance.PlaySoundEffect("Inhale");
        SoundManager.Instance.PlaySoundEffect("Inhale", _breathDuration/2);
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = _animationCurve.Evaluate(percent);
            transform.localScale = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }
        StartCoroutine(AnimateBreath(Vector2.one, Vector2.one * _scaleUp, _breathDuration));
    }

    

    IEnumerator AnimateStart(Vector2 origin, Vector2 target, float duration)
    {
        SoundManager.Instance.PlaySoundEffect("Tone");
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = _startAnimationCurve.Evaluate(percent);
            transform.localScale = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }
        if (started)
            StartCoroutine(AnimateBreath(Vector2.one, Vector2.one * _scaleUp, _breathDuration));
    }

    IEnumerator AnimateTextFade(Color origin, Color target, float duration)
    {
        if (started)
            yield return new WaitForSeconds(3f);
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            float curvePercent = _startAnimationCurve.Evaluate(percent);

            _startText.color = Color.LerpUnclamped(origin, target, curvePercent);
            yield return null;
        }
    }
}

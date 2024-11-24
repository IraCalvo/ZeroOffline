using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ItemBounce : MonoBehaviour
{
    [SerializeField] float minDuration;
    [SerializeField] float maxDuration;
    [SerializeField] float bounceStrengthMin;
    [SerializeField] float bounceStrengthMax;

    public void StartBounce(Vector2 pos)
    {
        StartCoroutine(BounceToPosition(pos));
    }

    IEnumerator BounceToPosition(Vector2 targetPos)
    {
        float duration = Random.Range(minDuration, maxDuration);
        float randomBounceStrength = Random.Range(bounceStrengthMin, bounceStrengthMax);
        float timeElapsed = 0f;

        Vector2 initialPosition = transform.position;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / duration;
            float bounceFactor = Mathf.Sin(t * Mathf.PI * 2) * randomBounceStrength;

            transform.position = Vector2.Lerp(initialPosition, targetPos, t) + new Vector2(0, bounceFactor * 0.2f);
            yield return null;
        }

        float smoothDuration = 0.1f;
        float smoothTime = 0f;

        Vector2 finalPos = targetPos;

        while (smoothTime < smoothDuration)
        {
            smoothTime += Time.deltaTime;
            float smoothT = smoothTime / smoothDuration;

            transform.position = Vector2.Lerp(transform.position, finalPos, smoothT);
            yield return null;
        }

        transform.position = finalPos;  
    }
}

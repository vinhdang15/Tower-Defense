using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
//using Image = UnityEngine.UI.Image;

public class PracticalUtilities : MonoBehaviour
{
    public Color originalColor;
    public IEnumerator ScaleUp(Transform targetTransform, Vector3 startScale, Vector3 endScale, float duration)
    {
        float currentTime = 0f;
        while(currentTime <= duration)
        {
            targetTransform.localScale = Vector2.Lerp(startScale, endScale, currentTime/duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        targetTransform.localScale = endScale;
    }

    public void ShowObjectCoroutine(Coroutine scaleUpCoroutine, Transform transform, Vector3 startScale, Vector3 endScale, float duration)
    {
        if(scaleUpCoroutine != null)
        {
            StopCoroutine(scaleUpCoroutine);
        }
        else
        {
            //scaleUpCoroutine = StartCoroutine(ScaleUp(transform, startScale, endScale, duration));
            StartCoroutine(ScaleUp(transform, startScale, endScale, duration));
        }
    }

    public void HideObjectCoroutine(Coroutine ScaleUpCoroutine, Transform transform, Vector3 startScale)
    {
        if(ScaleUpCoroutine != null)
        {
            StopCoroutine(ScaleUpCoroutine);
            Debug.Log("Stop Coroutine");
        }
        transform.localScale = startScale;
    }

    public void SetSprtieIndicator(Transform transform, float targetRange, float spriteBoundInX)
    {   
        float rangeIndicatorScale = targetRange / spriteBoundInX;
        transform.localScale = new Vector3(rangeIndicatorScale, rangeIndicatorScale, 1);
    }

    public void GreyOutImage(Transform item)
    {
        Image itemSprite = item.GetComponent<Image>();
        //originalColor = itemSprite.color;
        itemSprite.color = Color.gray;
    }

    public void ResetItemColor(Transform item)
    {
        Image itemSprite = item.GetComponent<Image>();
        //itemSprite.color = originalColor;
        itemSprite.color = Color.white;
    }
}

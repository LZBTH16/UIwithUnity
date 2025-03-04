using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform coinTextContainer;
    [SerializeField] private float duration;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = coinTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height + 60; // add '+ 60' as configuring the UI for my screen resulted in having to set offsets from the instructions
    }

    public void UpdateScore(int score)
    {
        // set the score to the masked text UI
        toUpdate.SetText($"{score}");
        // trigger the local move animations
        coinTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetCoinContainer(score));
    }

    private IEnumerator ResetCoinContainer(int score)
    {
        // let the editor know to wait for a given time period
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}"); // update the original score
        Vector3 localPosition = coinTextContainer.localPosition;
        coinTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition + 60, localPosition.z); // resets the y-localPosition of the coinTextContainer
    }
}

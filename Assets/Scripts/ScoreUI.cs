using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private PetTrain train;
    [SerializeField] private TextMeshProUGUI label;

    private int lastCount = -1;

    private void Update()
    {
        if (train == null || label == null) return;

        if (train.Count != lastCount)
        {
            lastCount = train.Score;
            label.text = "Score: " + lastCount;
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [Header("引用")]
    [SerializeField] private PetTrain train;
    [SerializeField] private PlayerController player;
    [SerializeField] private PetSpawner spawner;

    [Header("面板")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI scoreLabel;

    [Header("速度")]
    [SerializeField] private Button speedButton;
    [SerializeField] private TextMeshProUGUI speedLabel;

    [Header("倍率")]
    [SerializeField] private Button multiButton;
    [SerializeField] private TextMeshProUGUI multiLabel;

    [Header("刷新")]
    [SerializeField] private Button spawnButton;
    [SerializeField] private TextMeshProUGUI spawnLabel;

    private int speedLevel = 1;
    private int multiLevel = 1;
    private int spawnLevel = 1;

    private int SpeedCost => 5 * speedLevel;
    private int MultiCost => 12 * multiLevel;
    private int SpawnCost => 8 * spawnLevel;

    private void Start()
    {
        speedButton.onClick.AddListener(BuySpeed);
        multiButton.onClick.AddListener(BuyMulti);
        spawnButton.onClick.AddListener(BuySpawn);

        if (panel != null) panel.SetActive(false);
        Refresh();
    }

    public void Open()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        Refresh();
    }

    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void BuySpeed()
    {
        if (!train.TrySpendScore(SpeedCost)) return;
        speedLevel++;
        player.SetSpeed(player.MoveSpeed * 1.35f);
        Refresh();
    }

    private void BuyMulti()
    {
        if (!train.TrySpendScore(MultiCost)) return;
        multiLevel++;
        train.SetMultiplier(multiLevel);
        Refresh();
    }

    private void BuySpawn()
    {
        if (!train.TrySpendScore(SpawnCost)) return;
        spawnLevel++;
        spawner.SetSpawnInterval(spawner.SpawnInterval * 0.55f);
        Refresh();
    }

    private void Refresh()
    {
        int score = train.Score;

        if (scoreLabel != null)
            scoreLabel.text = "Score: " + score;

        speedLabel.text = "Speed  Lv" + speedLevel + "\ncost " + SpeedCost;
        multiLabel.text = "Multiplier x" + multiLevel + "\ncost " + MultiCost;
        spawnLabel.text = "Spawn Rate  Lv" + spawnLevel + "\ncost " + SpawnCost;

        speedButton.interactable = score >= SpeedCost;
        multiButton.interactable = score >= MultiCost;
        spawnButton.interactable = score >= SpawnCost;
    }
}
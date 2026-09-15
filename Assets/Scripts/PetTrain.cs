using System.Collections.Generic;
using UnityEngine;

public class PetTrain : MonoBehaviour
{
    [SerializeField] private float spacing = 1.2f;
    
    [SerializeField] private int scoreMultiplier = 1;
    private int score;

    public int Score => score;
    public int Multiplier => scoreMultiplier;

    public void SetMultiplier(int value)
    {
        scoreMultiplier = value;
    }

    private readonly List<Transform> anchors = new List<Transform>();

    public int Count => anchors.Count;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("撞到了: " + other.name);

        Pet pet = other.GetComponent<Pet>();
        if (pet == null) { Debug.Log("没有 Pet 脚本"); return; }
        if (pet.IsCollected) { Debug.Log("已经被收集过"); return; }

        Debug.Log("成功收集，当前第 " + (anchors.Count + 1) + " 只");

        Transform previous = anchors.Count == 0 ? transform : anchors[anchors.Count - 1];

        GameObject anchor = new GameObject("Anchor" + anchors.Count);
        anchor.transform.position = previous.position - transform.forward * spacing;

        Follower f = anchor.AddComponent<Follower>();
        f.Init(previous, spacing);

        anchors.Add(anchor.transform);
        pet.Collect(anchor.transform);
        score += scoreMultiplier;
    }
    
    public bool TrySpendScore(int amount)
    {
        if (score < amount) return false;
        score -= amount;
        return true;
    }
}

public class Follower : MonoBehaviour
{
    private Transform target;
    private float distance;

    public void Init(Transform t, float d)
    {
        target = t;
        distance = d;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 dir = transform.position - target.position;
        if (dir.sqrMagnitude < 0.0001f) dir = -target.forward;

        transform.position = target.position + dir.normalized * distance;
    }
}
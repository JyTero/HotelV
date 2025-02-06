using System;
using UnityEngine;
using System.Collections;

public class TickManager : MonoBehaviour
{
    public static TickManager Instance { get; private set; }

    public event Action<int> OnTick;
    [Tooltip("Ticks per second")]
    public int TickCounter { get; private set; } = 0;

    [SerializeField]
    private int tickRate; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(TickLoop());
    }

    private IEnumerator TickLoop()
    {
        float tickInterval = 1f / tickRate;

        while (true)
        {
            yield return new WaitForSeconds(tickInterval);
            TickCounter++;
            OnTick?.Invoke(TickCounter);
        }
    }
}

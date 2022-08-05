using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private float cycleDuration = 60f;
    [SerializeField] private TextMeshProUGUI resourcesText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI lastResourceRateText;

    private int lastResourceRate = 0;
    private bool cycleClipPlayed = false;
    private float timer;
    private Player player;

    #region Getters

    public int GetLastResourceRate()
    {
        return lastResourceRate;
    }

    public int GetResources()
    {
        return player.GetResources();
    }

    #endregion

    private void Start()
    {
        player = FindObjectOfType<Player>();
        timer = cycleDuration;
        lastResourceRateText.text = "Last rate: " + lastResourceRate.ToString();
    }

    private void Update()
    {
        resourcesText.text = "AER: " + player.GetResources().ToString();
        timerText.text = "Next Cycle in: " + Mathf.Floor(timer + 1).ToString() + " sec";
        timer -= Time.deltaTime;
        if (timer <= 3 && !cycleClipPlayed)
        {
            AudioPlayer.PlayNextCycleClip();
            cycleClipPlayed = true;
        }
        if (timer <= 0)
        {
            timer += cycleDuration;
            cycleClipPlayed = false;
            lastResourceRate = 0;
            foreach (Building building in FindObjectsOfType<Building>())
            {
                lastResourceRate += building.GetResourceRateByCycle();
            }
            lastResourceRateText.text = "Last rate: " + lastResourceRate.ToString();
            player.AddResources(lastResourceRate);
        }
    }
}

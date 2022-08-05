using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private AudioClip buttonHoverOverSound;
    [SerializeField][Range(0, 1)] float hoverOverVolume = 1f;

    [SerializeField] private AudioClip aerAddedSound;
    [SerializeField][Range(0, 1)] float aerAddedVolume = 1f;

    [SerializeField] private AudioClip notEnoughAERSound;
    [SerializeField][Range(0, 1)] float notEnoughAERVolume = 1f;

    [Header("Buildings")]
    [SerializeField] private AudioClip buildingPlacedSound;
    [SerializeField][Range(0, 1)] float buildingPlacedVolume = 1f;

    [SerializeField] private AudioClip buildingDestroyedSound;
    [SerializeField][Range(0, 1)] float buildingDestroyedVolume = 1f;

    [Header("Environment")]
    [SerializeField] private AudioClip nextCycleSound;
    [SerializeField][Range(0, 1)] float nextCycleVolume = 1f;

    public static AudioPlayer instance;

    private void Awake()
    {
        instance = this;
    }

    #region ClipPlayers

    public static void PlayHoverOverClip()
    {
        instance.PlayClip(instance.buttonHoverOverSound, instance.hoverOverVolume);
    }

    public static void PlayNotEnoughAERClip()
    {
        instance.PlayClip(instance.notEnoughAERSound, instance.notEnoughAERVolume);
    }

    public static void PlayBuildingPlacedClip()
    {
        instance.PlayClip(instance.buildingPlacedSound, instance.buildingPlacedVolume);
    }

    public static void PlayBuildingDestroyedClip()
    {
        instance.PlayClip(instance.buildingDestroyedSound, instance.buildingDestroyedVolume);
    }


    public static void PlayAerAddedClip()
    {
        instance.PlayClip(instance.aerAddedSound, instance.aerAddedVolume);
    }

    public static void PlayNextCycleClip()
    {
        FindObjectOfType<AudioSource>().PlayOneShot(instance.nextCycleSound, instance.nextCycleVolume);
    }

    #endregion

    private void PlayClip(AudioClip clipToPlay, float volume)
    {
        if (clipToPlay != null)
        {
            AudioSource.PlayClipAtPoint(
                clipToPlay,
                Camera.main.transform.position,
                volume);
        }
    }
}

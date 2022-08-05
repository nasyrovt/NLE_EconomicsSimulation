using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonCharacteristics : MonoBehaviour
{
    [SerializeField] private Building associatedBuilding;
    [SerializeField] private string buildingName;
    [SerializeField] private BuildingResourceType[] resourceTypes;
    [SerializeField] private Image icon;
    [SerializeField] private ParticleSystem explosionVFX;


    #region Getters&Setters

    public Building GetAssociatedBuilding()
    {
        return associatedBuilding;
    }

    public Image GetIcon()
    {
        return icon;
    }

    public void SetAssociatedBuilding(Building building)
    {
        associatedBuilding = building;
    }

    public void SetBuildingName(string name)
    {
        buildingName = name;
    }

    public void SetResourcesType(BuildingResourceType[] types)
    {
        resourceTypes = types;
    }

    #endregion


    #region DestroyBuildingAndSelf

    public void DestroyAssociatedBuilding()
    {
        AudioPlayer.PlayBuildingDestroyedClip();
        FindObjectOfType<CameraShake>().Play();
        Invoke(nameof(PlayExplosionEffectAndDestroyBuilding), 0.3f);
    }

    private void PlayExplosionEffectAndDestroyBuilding()
    {
        Instantiate(explosionVFX, associatedBuilding.transform.position, Quaternion.identity);
        Destroy(associatedBuilding.gameObject);
        Destroy(gameObject);
    }

    #endregion
}

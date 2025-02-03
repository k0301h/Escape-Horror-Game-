using UnityEngine;

public class UnitSpawnEvent : MonoBehaviour
{
    [SerializeField] private GameObject unitGameObject;
    [SerializeField] private Transform unitParentTransform;
    [SerializeField] private Transform unitPosition;
    
    public void SpawnUnit()
    {
        GameObject instance = Instantiate(unitGameObject, unitParentTransform);
        instance.transform.position = unitPosition.position;
        instance.transform.rotation = unitPosition.rotation;
    }

    public void DestroyUnitToString(string unitName)
    {
        Destroy(gameObject.transform.parent.Find(unitName).gameObject);
    }
    
    public void DestroyUnitToGameObject(GameObject unitGameObject)
    {
        Destroy(unitGameObject);
    }
}
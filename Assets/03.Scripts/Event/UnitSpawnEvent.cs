using UnityEngine;

public class UnitSpawnEvent : MonoBehaviour
{
    [SerializeField] private GameObject unitGameObject;
    [SerializeField] private Transform unitParentTransform;
    [SerializeField] private Transform unitPosition;
    private bool _isAnimation = false;

    public void SetAnimation(bool isAnimation)
    {
        _isAnimation = isAnimation;
    }
    
    public void SpawnUnit()
    {
        GameObject instance = Instantiate(unitGameObject, unitParentTransform);
        instance.transform.position = unitPosition.position;
        instance.transform.rotation = unitPosition.rotation;

        if (_isAnimation)
        {
            if (instance.TryGetComponent(out Manequin manequin))
            {
                manequin.PlayRotation();
            }
        }
    }

    public void DestroyUnitToString(string unitName)
    {
        Destroy(gameObject.transform.Find(unitName).gameObject);
    }
    
    public void DestroyUnitToGameObject(GameObject unitGameObject)
    {
        Destroy(unitGameObject);
    }
}
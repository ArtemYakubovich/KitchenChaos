using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaceHere;
    
    public static void ResetStaticData()
    {
        OnAnyObjectPlaceHere = null;
    }
    
    [SerializeField] private Transform _counterTopPoint;
    
    private KitchenObject _kitchenObject;
    
    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter.Interact();");
    }
    
    public virtual void InteractAlternate(Player player)
    {
        //Debug.Log("BaseCounter.InteractAlternate();");
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnAnyObjectPlaceHere.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}

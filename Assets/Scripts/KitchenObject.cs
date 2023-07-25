using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private ClearCounter _clearCounter;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }
    
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this._clearCounter != null)
        {
            this._clearCounter.ClearKitchenObject();
        }
        this._clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has KitchenObject!");
        }
        
        clearCounter.SetKitchenObject(this);
        
        transform.parent = _clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}

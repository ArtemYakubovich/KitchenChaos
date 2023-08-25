using System;
using UnityEngine;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredienAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;

    private List<KitchenObjectSO> _kitchenObjectSOList;

    private void Awake()
    {
        _kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) 
    {
        if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        
        if(_kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            _kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredienAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return _kitchenObjectSOList;
    }
}

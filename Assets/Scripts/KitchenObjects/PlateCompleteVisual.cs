using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject _plateKitchenObjekt;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectList;

    private void Start()
    {
        _plateKitchenObjekt.OnIngredienAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
            print(kitchenObjectSOGameObject.gameObject.name);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList)
        {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}

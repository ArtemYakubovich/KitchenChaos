using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter _platesCounter;
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;

    private List<GameObject> _plateVisualGameobjectList;

    private void Awake()
    {
        _plateVisualGameobjectList = new List<GameObject>();
    }

    private void Start()
    {
        _platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = _plateVisualGameobjectList[_plateVisualGameobjectList.Count - 1];
        _plateVisualGameobjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

        float plateOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffset * _plateVisualGameobjectList.Count, 0f);
        _plateVisualGameobjectList.Add(plateVisualTransform.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Coin;
using PlayingField;
using UnityEngine;
using Zenject;

public class PlayingFieldView : MonoBehaviour
{
    public Transform Colums => colums;
    public CoinSpawnView[] CoinSpawPoint => coinSpawPoint;
    public CurrentCoinPointView CurrentCoinPoint => currentCoinPoint;
    
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject leftBox;
    [SerializeField] private GameObject rightBox;
    [SerializeField] private GameObject plane;
    
    [SerializeField] private Transform colums;
    [SerializeField] private CoinSpawnView[] coinSpawPoint;
    [SerializeField] private CurrentCoinPointView currentCoinPoint;
    
    private Vector3 position = new Vector3(7.5f, 9.8f, 0f);

    private void ReInit(PlayingFieldModel item)
    {
        cylinder.transform.localScale = item.CylinderTransform.localScale;
        cylinder.transform.localPosition = item.CylinderTransform.localPosition;
        cylinder.transform.rotation = item.CylinderTransform.rotation;
        
        leftBox.transform.localScale = item.LeftBoxTransform.localScale;
        leftBox.transform.localPosition = item.LeftBoxTransform.localPosition;
        leftBox.transform.rotation = item.LeftBoxTransform.rotation;
        
        rightBox.transform.localScale = item.RightBoxTransform.localScale;
        rightBox.transform.localPosition = item.RightBoxTransform.localPosition;
        rightBox.transform.rotation = item.RightBoxTransform.rotation;
        
        plane.transform.localScale = item.PlaneTransform.localScale;
        plane.transform.localPosition = item.PlaneTransform.localPosition;
        plane.transform.rotation = item.PlaneTransform.rotation;
    }
    public class  Pool : MonoMemoryPool<PlayingFieldModel,PlayingFieldView>
    {
        protected override void Reinitialize(PlayingFieldModel material, PlayingFieldView item)
        {
            item.ReInit(material);
            item.transform.position = item.position;
        }
    }
}
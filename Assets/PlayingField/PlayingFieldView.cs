using System.Collections;
using System.Collections.Generic;
using Coin;
using PlayingField;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayingFieldView : MonoBehaviour
{
    public Transform Colums => colums;
    public CoinSpawnView[] CoinSpawPoint => coinSpawPoint;
    public CurrentCoinPointView CurrentCoinPoint => currentCoinPoint;
    
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject leftSphere;
    [SerializeField] private GameObject rightSphere;
    
    [SerializeField] private Transform colums;
    [SerializeField] private CoinSpawnView[] coinSpawPoint;
    [SerializeField] private CurrentCoinPointView currentCoinPoint;
    
    private Vector3 position = new Vector3(7.5f, 9.8f, 0f);

    private void ReInit(PlayingFieldModel item)
    {
        cylinder.transform.localScale = item.CylinderTransform.localScale;
        cylinder.transform.localPosition = item.CylinderTransform.localPosition;
        cylinder.transform.rotation = item.CylinderTransform.rotation;
        
        leftSphere.transform.localPosition = item.LeftSpherePosition;
        rightSphere.transform.localPosition = item.RightSpherePosition;
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
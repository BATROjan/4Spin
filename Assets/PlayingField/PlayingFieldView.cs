using System.Collections;
using System.Collections.Generic;
using PlayingField;
using UnityEngine;
using Zenject;

public class PlayingFieldView : MonoBehaviour
{
    public Transform Colums => colums;
    public CoinSpawnView[] CoinSpawPoint => coinSpawPoint;
    
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    
    [SerializeField] private Transform colums;
    [SerializeField] private CoinSpawnView[] coinSpawPoint;
    [SerializeField] private GameObject[] arrows;
    
    private Vector3 position = new Vector3(7.5f, 9.8f, 0f);

    public GameObject[] GetArrows()
    {
        return arrows;
    }
    private void ReInit(PlayingFieldModel item)
    {
        cylinder.transform.position = item.CylindrTransfom.position;
        cylinder.transform.localScale = item.CylindrTransfom.localScale;
        cylinder.transform.rotation = item.CylindrTransfom.rotation;
    }
    public class  Pool : MonoMemoryPool<PlayingFieldModel,PlayingFieldView>
    {
        protected override void Reinitialize(PlayingFieldModel playingFieldModel, PlayingFieldView item)
        {
            item.ReInit(playingFieldModel);
            item.transform.position = item.position;

        }
    }
}
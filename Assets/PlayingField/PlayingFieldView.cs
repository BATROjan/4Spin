using System.Collections;
using System.Collections.Generic;
using PlayingField;
using UnityEngine;
using Zenject;

public class PlayingFieldView : MonoBehaviour
{
    public Transform Colums => colums;
    
    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private Transform colums;
    
    private Vector3 position = new Vector3(3.27f, 7.73f, -0.28f);

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
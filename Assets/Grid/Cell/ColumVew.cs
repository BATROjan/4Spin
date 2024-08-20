using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ColumVew : MonoBehaviour
{
    public Transform ColumTranform;
    public BoxCollider BoxCollider;
    public int ColumID;
    
    public class  Pool : MonoMemoryPool<ColumVew>
    {
    }
}

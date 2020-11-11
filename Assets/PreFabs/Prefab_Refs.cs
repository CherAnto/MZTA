using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefab_Refs", menuName = "Prefab_Refs", order = 51)]
public class Prefab_Refs : ScriptableObject
{
    public GameObject ItemPF => _itemPF;
    [SerializeField] GameObject _itemPF;


}

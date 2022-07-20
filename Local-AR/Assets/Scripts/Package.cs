using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Package : MonoBehaviour
{
    public enum PackageType { DealDamage, ReceivedDamage, MonsterSwap, ItemUse }

    public PackageType packageType;
}

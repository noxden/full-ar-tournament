using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Package
{
    public enum PackageType { JoinPackage, CombatPackage, /*MonsterSwap*/ }

    public PackageType packageType;
}

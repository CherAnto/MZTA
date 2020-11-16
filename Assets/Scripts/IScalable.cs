using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScalable
{
    Vector3 scale { get; }
    void SetScale(Vector3 scale);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    Vector3 position { get; }
    void Move(Vector3 moveBy);
    void SetParent(Transform transform);
    void SetPosition(Vector3 position);
}

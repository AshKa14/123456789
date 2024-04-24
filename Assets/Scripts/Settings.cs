using System;
using System.Collections.Generic;
using Object = System.Object;
using UnityEngine;

[Serializable]
public class Settings : Object
{
  public float shipSpeed;
  public List<float> asteroidSpeed;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}

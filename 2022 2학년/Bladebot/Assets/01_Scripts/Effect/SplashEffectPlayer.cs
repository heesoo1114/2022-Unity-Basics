using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SplashEffectPlayer : EffectPlayer
{
    private readonly int _deltaYHASH = Shader.PropertyToID("YDelta");
    private readonly int _colorHash = Shader.PropertyToID("Color");
    private readonly int _hitNormalHash = Shader.PropertyToID("HitNormal");

    public void SetData(Color color, float yDelta, Vector3 hitNormal)
    {
        foreach (VisualEffect e in _effects)
        {
            e.SetFloat(_deltaYHASH, yDelta);
            e.SetVector4(_colorHash, color);
            e.SetVector3(_hitNormalHash, hitNormal);
        }
    }
}

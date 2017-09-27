using UnityEngine;
using System.Collections;

public interface IHealable<T>
{
    int Hp { get; set; }

    void Heal(T hpAmount);
}

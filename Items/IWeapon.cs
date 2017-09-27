
using System.Collections.Generic;

public interface IWeapon
{
    List<BaseStat> Stats { get; set; }

    void Attack();

    //void Attack2
}
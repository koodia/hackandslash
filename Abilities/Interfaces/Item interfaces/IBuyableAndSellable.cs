using UnityEngine;
using System.Collections;

public interface IBuyableAndSellable
{
    int price { get; set; }

    void Buy();
    void Sell();
}


public interface IDamageable<T>
{
    int Hp { get; set; }

    void Damage(T damageAmount); //Amount
}
public interface IDamage
{
    bool IsAlive
    { get; set; }

    bool TakeDamage(float pDamage);
}


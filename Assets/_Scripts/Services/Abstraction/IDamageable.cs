namespace Services.Abstraction
{
    public interface IDamageable
    {
        bool IsDead { get; }
        void TakeDamage(float damage);
        void Die();
    }
}
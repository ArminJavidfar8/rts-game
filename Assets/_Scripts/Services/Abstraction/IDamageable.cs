namespace Services.Abstraction
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void Die();
    }
}
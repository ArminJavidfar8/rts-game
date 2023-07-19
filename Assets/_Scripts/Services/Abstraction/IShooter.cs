namespace Services.Abstraction
{
    public interface IShooter
    {
        void Shoot(IDamageable target, int damage);
        IDamageable FindDamagable();
    }
}
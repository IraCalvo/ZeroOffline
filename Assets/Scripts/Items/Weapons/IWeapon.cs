public interface IWeapon : IItem
{
    WeaponSO WeaponSO { get; }
    public void UseWeapon();
}

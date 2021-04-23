// A UnitInstance represents an active enemy of the given unit type.
public class UnitInstance {
    public Unit unit;
    public float health;

    public UnitInstance(Unit unit) {
        this.unit = unit;
        this.health = unit.maxHealth;
    }

    public void TakeDamage(Weapon fromWeapon) {
        this.health -= fromWeapon.calculateDamageGiven(unit.armor);
    }
}
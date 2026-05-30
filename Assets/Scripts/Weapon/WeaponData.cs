using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    public string weaponName;

    [Header("Combat")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 100f;

    [Header("Ammo")]
    public int magazineSize = 30;
    public float reloadTime = 2f;

    [Header("Recoil")]
    public float recoilAmount = 1f;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 80f;

    //Impelement Effects later
    //[Header("Effects")]
    //public GameObject muzzleFlashPrefab;
    //public GameObject hitEffectPrefab;
}
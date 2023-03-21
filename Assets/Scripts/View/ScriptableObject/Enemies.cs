using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies", menuName = "Enemies/CreateEnemy")]
public class Enemies : ScriptableObject
{
    public EnemyView Template;
    public string Name;
    public float Damage;
    public float Speed;       
}

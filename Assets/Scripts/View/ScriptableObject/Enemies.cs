using UnityEngine;

[CreateAssetMenu(fileName = "New Enemies", menuName = "Enemies/CreateEnemy")]
public class Enemies : ScriptableObject
{
    public EnemyView Template;
    public string Name;
    public int Damage;
    public int Speed;       
}

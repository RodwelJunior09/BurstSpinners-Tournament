using UnityEngine;

public class Character : ScriptableObject
{
    public int health;
    public double spinHealth;
    public int timeToCooldown;
    public int durationOfHability;
    
    [SerializeField, Range(1, 10)] public float speed;
}

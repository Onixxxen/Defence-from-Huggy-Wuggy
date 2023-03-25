using UnityEngine;

public class RecoveryPresenter
{
    private RecoveryHealthView _recoveryHealth;
    private RecoveryArmorView _recoveryArmor;
    private Health _health;
    private Armor _armor;
    private BrainView _brainView;

    public void Init(RecoveryHealthView recoveryHealth, RecoveryArmorView recoveryArmor, Health health, Armor armor, BrainView brainView)
    {
        _recoveryHealth = recoveryHealth;
        _recoveryArmor = recoveryArmor;
        _health = health;
        _armor = armor;
        _brainView = brainView;
    }

    public void Enable()
    {
        _recoveryHealth.TryRecoveryHealth += RequestRecoveryHealth;
        _recoveryArmor.TryRecoveryArmor += RequestRecoveryArmor;

        _health.RecoveryHealth += OnRecoveryHealth;
        _armor.RecoveryArmor += OnRRecoveryArmor;
    }

    public void Disable()
    {
        _recoveryHealth.TryRecoveryHealth += RequestRecoveryHealth;
        _recoveryArmor.TryRecoveryArmor += RequestRecoveryArmor;

        _health.RecoveryHealth -= OnRecoveryHealth;
        _armor.RecoveryArmor -= OnRRecoveryArmor;
    }

    private void RequestRecoveryHealth()
    {
        _health.RecoveryHealthRequest();
    }

    private void RequestRecoveryArmor()
    {
        _armor.RecoveryArmorRequest();
    }

    private void OnRecoveryHealth(int newHealth)
    {
        _brainView.ChangeHealthValue(newHealth);
        _recoveryHealth.BlockRecoveryButton();
    }

    private void OnRRecoveryArmor(int newArmor)
    {
        _brainView.ChangeArmorValue(newArmor);
        _recoveryArmor.BlockRecoveryButton();
    }
}

using UnityEngine;
using YG;

public class BrainPresenter
{
    private Health _health;
    private Armor _armor;
    private BrainView _brainView;
    private DayChanger _dayChanger;

    public void Init(Health health, Armor armor, BrainView brainView, DayChanger dayChanger)
    {
        _health = health;
        _armor = armor;
        _brainView = brainView;
        _dayChanger = dayChanger;
    }

    public void Enable()
    {
        _brainView.OnRestoreBrain += RequestRestoreBrain;

        _armor.GiveArmorValue += OnGiveArmorValue;
        _health.GiveHealthValue += OnGiveHealthValue;
        _armor.GiveArmorCount += OnGiveArmorCount;
        _health.GiveHealthCount += OnGiveHealthCount;
        _dayChanger.RestoreBrain += OnRestoreBrain;
    }
    public void Disable()
    {
        _brainView.OnRestoreBrain -= RequestRestoreBrain;

        _armor.GiveArmorValue -= OnGiveArmorValue;
        _health.GiveHealthValue -= OnGiveHealthValue;
        _armor.GiveArmorCount -= OnGiveArmorCount;
        _health.GiveHealthCount -= OnGiveHealthCount;
        _dayChanger.RestoreBrain -= OnRestoreBrain;
    }

    private void RequestArmorValue()
    {
        _armor.ArmorValueRequest();
    }

    private void RequestHealthValue()
    {
        _health.HealthValueRequest();
    }

    private void RequestRestoreBrain()
    {
        _health.RestoreHealth();
        _armor.RestoreArmor();
    }

    private void OnGiveArmorValue(int value)
    {
        _brainView.SetArmorValue(value);
    }

    private void OnGiveHealthValue(int value)
    {
        _brainView.SetHealthValue(value);
    }    

    private void OnGiveArmorCount(int value, int maxValue)
    {
        _brainView.ChangeArmorCount(value, maxValue);
    }

    private void OnGiveHealthCount(int value, int maxValue)
    {
        _brainView.ChangeHealthCount(value, maxValue);
    }

    private void OnRestoreBrain(int healthValue, int armorValue)
    {
        _brainView.SetHealthValue(healthValue);
        _brainView.SetArmorValue(armorValue);
    }
}

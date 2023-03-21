public class Armor : Brain
{
    private int _count = 10;

    public int Count => _count;

    public void ChangeArmorCount(int count)
    {
        _count += count;
    }
}

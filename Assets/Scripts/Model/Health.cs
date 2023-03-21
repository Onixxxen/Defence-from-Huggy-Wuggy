public class Health : Brain
{
    private int _count = 10;

    public int Count => _count;

    public void ChangeHealthCount(int count)
    {
        _count += count;
    }
}

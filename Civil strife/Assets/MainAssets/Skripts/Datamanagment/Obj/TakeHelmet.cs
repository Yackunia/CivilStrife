
public class TakeHelmet : GetObj
{
    protected override void TakeThis()
    {
        if (!inv.helmets[id])
        {
            inv.AddHelmet(id);
            Destroy(gameObject);
        }
    }
}

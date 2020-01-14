namespace Boven.Areas.WData.BovenDB
{
    public interface ISeedBoven
    {
        void EnsurePopulated(BovenContext context);
    }
}

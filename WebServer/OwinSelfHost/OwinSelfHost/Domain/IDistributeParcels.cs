namespace OwinSelfHost.Domain
{
    public interface IDistributeParcels
    {
        Parcel[] Distribute(string data);
    }
}
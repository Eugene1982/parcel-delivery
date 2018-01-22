namespace OwinSelfHost.Domain
{
    public class Parcel
    {
        public double Weight { get; set; }
        public double Price { get; set; }
        public Sender From { get; set; }
        public Receipient To { get; set; }
        public string DepartmentName { get; set; }
    }
}

namespace OwinSelfHost.Domain
{
    public class Parcel
    {
        public double Weight { get; set; }
        public double Price { get; set; }
        public Contact From { get; set; }
        public Contact To { get; set; }
        public string DepartmentName { get; set; }
    }
}

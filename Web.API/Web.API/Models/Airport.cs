namespace Web.API.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string IATACode { get; set; }
        public int GeographyLevel1ID { get; set;}
        public string Type { get; set;}

    }
}

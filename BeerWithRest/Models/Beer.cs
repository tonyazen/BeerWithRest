
namespace BeerWithRest.Models
{
    public class Beer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string Abv { get; set; }
        public string BreweryId { get; set; }
    }
}
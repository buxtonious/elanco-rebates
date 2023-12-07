namespace Er.Core.Helpers
{
    public class CountryHelper
    {
        private readonly List<string> countries;

        public CountryHelper()
        {
            countries = GenerateCountries();
        }

        public bool IsInList(string country)
        {
            return countries.Any(x => x.Equals(country));
        }

        public string Find(string salutation)
        {
            return countries.FirstOrDefault(x => x.Equals(salutation));
        }

        public List<string> ListAll()
        {
            return countries;
        }

        private List<string> GenerateCountries()
        {
            return new List<string>()
            {
                "United States",
                "United Kingdom",
                "Canada",
                "Germany",
                "Austrailia"
            };
        }
    }
}

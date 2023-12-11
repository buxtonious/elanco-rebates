namespace Er.Core.Helpers
{
    public class SalutationHelper
    {
        private readonly List<string> salutations;

        public SalutationHelper()
        {
            salutations = GenerateSalutations();
        }

        public bool IsInList(string salutation)
        {
            return salutations.Any(x => x.Equals(salutation));
        }

        public string Find(string salutation)
        {
            return salutations.FirstOrDefault(x => x.Equals(salutation));
        }

        public List<string> ListAll()
        {
            return salutations;
        }

        private List<string> GenerateSalutations()
        {
            return new List<string>()
            {
                "Mr",
                "Mrs",
                "Ms",
                "Miss",
                "Dr"
            };
        }
    }
}

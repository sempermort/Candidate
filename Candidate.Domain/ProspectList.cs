namespace Candidate.Domain
{
    public class ProspectList
    {
        private readonly List<Prospect> _pros = new();

        public IReadOnlyCollection<Prospect> pros => _pros.AsReadOnly();

        public void AddApplicant(Prospect prospect)
        {
            _pros.Add(prospect);
        }
    }
}

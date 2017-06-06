namespace FluentFilter.Inetnal.Visitors
{
    internal class DataFilterVisitor
    {
        private DataFilterVisitorProvider _provider;
        public DataFilterVisitor()
        {
            _provider = new DataFilterVisitorProvider();
        }
        public DataFilterVisitorProvider Provider { get => _provider; }
    }
}

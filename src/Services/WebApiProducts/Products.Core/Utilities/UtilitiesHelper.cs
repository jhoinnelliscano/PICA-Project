
namespace Products.Core.Utilities
{
    public static class UtilitiesHelper
    {
        public enum ORDERBY
        {
            MinPrice,
            MaxPrice,
            Relevance
        }

        public enum CONDITION
        {
            New,
            Used,
            Returned
        }

        public enum CONDITIONCODE
        {
            NUV,
            USA,
            DEV
        }
    }
}

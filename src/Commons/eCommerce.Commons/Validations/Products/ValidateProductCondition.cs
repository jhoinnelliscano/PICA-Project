using eCommerce.Commons.Utilities;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Validations.Products
{
    public class ValidateProductCondition : ValidationAttribute
    {
        public ValidateProductCondition() { }

        public override bool IsValid(object? value)
        {
            try
            {
                var sort = value as string;

                if (string.IsNullOrEmpty(sort)) return false;

                if (sort == ProductUtilities.CONDITION.Used.ToString() || sort == ProductUtilities.CONDITION.Returned.ToString()
                    || sort == ProductUtilities.CONDITION.New.ToString())
                    return true;

                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

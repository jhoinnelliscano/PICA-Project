
using Products.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Products.Core.Validations
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

                if (sort == UtilitiesHelper.CONDITION.Used.ToString() || sort == UtilitiesHelper.CONDITION.Returned.ToString()
                    || sort == UtilitiesHelper.CONDITION.New.ToString())
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

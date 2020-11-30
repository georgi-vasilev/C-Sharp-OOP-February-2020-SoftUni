namespace ValidationAttributes.Attributes
{
    public class MyRequiredAttribute : MyValidatorAttribute
    {
        public override bool IsValid(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return true;
        }
    }
}

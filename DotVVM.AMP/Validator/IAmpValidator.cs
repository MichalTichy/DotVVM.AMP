namespace DotVVM.AMP.Validator
{
    public interface IAmpValidator
    {
        bool CheckAttribute(string name, string value);
        bool CheckStyleAttribute(string name, string value);
        bool ValidateKnockoutDataBind(string name);
    }
}
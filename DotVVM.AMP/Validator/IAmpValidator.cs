using System.Collections.Generic;
using System.Threading.Tasks;
using ExCSS;

namespace DotVVM.AMP.Validator
{
    public interface IAmpValidator
    {
        bool CheckHtmlTag(string tagName, IDictionary<string,string> attributes);
        bool CheckAttribute(string attributeName, string attributeValue);
        bool CheckStylesheet(Stylesheet stylesheet);
        bool CheckStyleAttribute(string name, string value);
        bool ValidateKnockoutDataBind(string name);
    }
}
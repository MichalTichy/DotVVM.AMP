using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using ExCSS;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.Validator
{
    public class AmpValidator : IAmpValidator
    {
        private readonly DotvvmAmpConfiguration configuration;
        private readonly ILogger logger;


        protected Dictionary<string, string> ReplacedHtmlTags = new Dictionary<string, string>()
        {
            {"img", "amp-img"},
            {"video", "amp-video"},
            {"audio", "amp-audio"},
            {"iframe", "amp-iframe"}
        };

        protected string[] ForbiddenTags = new[] {"base", "picture", "frame", "frameset", "object", "param", "applet"};
        protected string[] IgnoredBindings = new[] {"text", "withGridViewDataSet", "dotvvm-SSR-foreach" };

        public AmpValidator(DotvvmAmpConfiguration configuration, ILogger<AmpValidator> logger = null)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public virtual bool CheckHtmlTag(string tagName, IDictionary<string, string> attributes)
        {
            if (!CheckForForbiddenHtmlTag(tagName)) return false;

            if (!CheckForRestrictedHtmlTag(tagName, attributes)) return false;

            if (!CheckForReplacedHtmlTags(tagName)) return false;

            if (!CheckLayoutAttribute(attributes)) return false;
            return true;
        }

        private bool CheckLayoutAttribute(IDictionary<string, string> attributes)
        {
            var hasLayout = attributes.ContainsKey("layout");
            if (!hasLayout) return true;

            var hasHeight = attributes.ContainsKey("height");
            var hasWidth = attributes.ContainsKey("width");

            var layout = attributes["layout"];

            var error = string.Empty;
            if ((layout=="fixed" || layout=="responsive" || layout=="intrinsic") && (!hasHeight || !hasWidth))
            {
                error = $"When layout is set to {layout}, than both height and width are required!";
            }
            else if (layout=="fixed-height" && !hasHeight)
            {
                error = $"When layout is set to {layout}, than both height is required!";
            }


            if (!string.IsNullOrWhiteSpace(error))
            {
                switch (configuration.HtmlTagHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException(error);
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogError(error);
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Unsuported {nameof(configuration.HtmlTagHandlingMode)}");
                }
            }

            return true;

        }

        protected virtual bool CheckForReplacedHtmlTags(string tagName)
        {
            var isValid = true;
            string errorMessage = string.Empty;
            if (ReplacedHtmlTags.ContainsKey(tagName.ToLower()))
            {
                isValid = false;
                errorMessage =
                    $"Html tag {tagName} was replaced by {ReplacedHtmlTags[tagName.ToLower()]} in amp pages!";
            }

            if (!isValid)
            {
                switch (configuration.HtmlTagHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException(errorMessage);
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogError(errorMessage);
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Unsuported {nameof(configuration.HtmlTagHandlingMode)}");
                }
            }

            return true;
        }

        protected virtual bool CheckForRestrictedHtmlTag(string tagName, IDictionary<string, string> attributes)
        {
            bool isRestrictedTagValid = true;
            string restrictedTagErrorMessage = string.Empty;
            switch (tagName.ToLower())
            {
                case string script when script == "script":
                    isRestrictedTagValid = attributes.ContainsKey("type") && (
                                           attributes["type"].ToLower() ==
                                           "application/ld+json".Replace(" ", string.Empty) || attributes["type"].ToLower()== "application/json".Replace(" ", string.Empty)) ||
                                           (attributes.ContainsKey("src") &&
                                            attributes["src"].ToLower() == DotvvmAmpConfiguration.AmpJsUrl) || attributes["src"].ToLower().StartsWith(DotvvmAmpConfiguration.AmpCdnUrl);
                    restrictedTagErrorMessage =
                        $@"Html tag script is valid only with type set to application/ld+json or with src set to {DotvvmAmpConfiguration.AmpJsUrl}!";
                    break;
                case string input when input == "input":
                    isRestrictedTagValid = !(attributes.ContainsKey("type") &&
                                             (attributes["type"].ToLower() == "image" ||
                                              attributes["type"].ToLower() == "button" ||
                                              attributes["type"].ToLower() == "password" ||
                                              attributes["type"].ToLower() == "file"));
                    restrictedTagErrorMessage =
                        $@"Html tag script is invalid with type set to image, button, password or file!";

                    break;
                case string style when style == "style":
                    isRestrictedTagValid =
                        attributes.ContainsKey("amp-custom") || attributes.ContainsKey("amp-boilerplate");
                    restrictedTagErrorMessage =
                        $@"Only single amp-boilerplate style or single amp-custom style is valid per page!";
                    break;
            }

            if (!isRestrictedTagValid)
            {
                switch (configuration.HtmlTagHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException(restrictedTagErrorMessage);
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogError(restrictedTagErrorMessage);
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Unsuported {nameof(configuration.HtmlTagHandlingMode)}");
                }
            }

            return true;
        }

        protected virtual bool CheckForForbiddenHtmlTag(string tagName)
        {
            if (ForbiddenTags.Contains(tagName.ToLower()))
            {
                var errorMessage = $"Html tag {tagName} is not allowed!";
                switch (configuration.HtmlTagHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException(errorMessage);
                    case ErrorHandlingMode.LogAndIgnore:
                        logger?.LogError(errorMessage);
                        return false;
                    default:
                        throw new ArgumentOutOfRangeException(
                            $"Unsuported {nameof(configuration.HtmlTagHandlingMode)}");
                }
            }

            return true;
        }

        public virtual bool CheckAttribute(string attributeName, string attributeValue)
        {
            var normalizedAttr = attributeName.ToLower();
            var isAttributeNameValid = normalizedAttr != "xmlns" &&
                                       !normalizedAttr.StartsWith("xml:");

            var isIdAttributeValid= attributeName != "id" || !attributeValue.ToLower().StartsWith("amp-") && !attributeValue.ToLower().StartsWith("i-amp-");
            if (isAttributeNameValid && isIdAttributeValid)
                return true;

            var exceptionMessage = $"Attribute {attributeName} is not valid {(!isIdAttributeValid? $"with value {attributeValue}" : "")}!";
            
            switch (configuration.AttributeHandlingMode)
            {
                case ErrorHandlingMode.LogAndIgnore:
                    logger?.LogError(exceptionMessage);
                    return false;
                case ErrorHandlingMode.Throw:
                    throw new AmpException(exceptionMessage);
                default:
                    throw new ArgumentOutOfRangeException($"Unsuported {nameof(configuration.AttributeHandlingMode)}");
            }

        }

        public virtual bool CheckStylesheet(Stylesheet stylesheet)
        {
            bool allValid = true;
            var errors = new List<string>();
            foreach (var stylesheetStyleRule in stylesheet.StyleRules.OfType<StyleRule>())
            {
                var isSelectorValid = !stylesheetStyleRule.SelectorText.Contains(".-amp-") && !stylesheetStyleRule.SelectorText.Contains(".i-amp-");
                if (!isSelectorValid)
                {
                    errors.Add("Found css selector targets -amp- or i-amp- tag. Targeting such elements is not supported!");
                    allValid = false;
                    (stylesheetStyleRule.Parent as StylesheetNode)?.RemoveChild(stylesheetStyleRule);
                    break;
                }

                foreach (var property in stylesheetStyleRule.Children.OfType<Property>())
                {
                    if (property.IsImportant)
                    {
                        property.IsImportant = false;
                        errors.Add($"Important css properties are not allowed! {property.Name}:{property.Value} !important");
                        allValid = false;
                    }

                    if (property.Name.ToLower().StartsWith("overflow") && (property.Value.ToLower()=="auto"  || property.Value.ToLower()=="scroll"))
                    {
                        errors.Add($"overflow css properties cannot have value auto or scroll! {property.Name}:{property.Value}");
                        stylesheetStyleRule?.RemoveChild(property);
                        allValid = false;
                    }

                    //TODO validate animations
                }
            }

            if (!allValid)
            {
                switch (configuration.StylesHandlingMode)
                {
                    case ErrorHandlingMode.Throw:
                        throw new AmpException(string.Join("\n",errors));
                    case ErrorHandlingMode.LogAndIgnore:
                        foreach (var error in errors)
                        {
                            logger?.LogError(error);
                            return false;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return true;
        }


        public virtual bool CheckStyleAttribute(string name, string value)
        {
            var stylesheet = new StylesheetParser().Parse($"{name}:{value};");
            return CheckStylesheet(stylesheet);
        }

        public virtual bool ValidateKnockoutDataBind(string name)
        {
            if (IgnoredBindings.Contains(name))
            {
                logger?.LogWarning($"{name} binding is ignored");
                return false;
            }
            
            var errorMessage = $"Control tried to use knockout dataBind {name}, which is unsupported during amp rendering.";

            switch (configuration.KnockoutHandlingMode)
            {
                case ErrorHandlingMode.Throw:
                    throw new AmpException(errorMessage);
                case ErrorHandlingMode.LogAndIgnore:
                    logger?.LogError(errorMessage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}
using System;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using Microsoft.Extensions.Logging;

namespace DotVVM.AMP.Validator
{
    public class AmpValidator : IAmpValidator
    {
        private readonly DotvvmAmpConfig config;
        private readonly ILogger logger;

        public AmpValidator(DotvvmAmpConfig config, ILogger logger)
        {
            this.config = config;
            this.logger = logger;
        }
        public bool CheckAttribute(string attributeName, string attributeValue)
        {
            var normalizedAttr = attributeName.ToLower();
            var isValid = !normalizedAttr.StartsWith("on") && normalizedAttr != "xmlns" && !normalizedAttr.StartsWith("xml:") && !normalizedAttr.StartsWith("i-amp-");

            if (isValid)
            {
                return true;
            }

            var exceptionMessage = $"Attribute {attributeName} is not valid!";

            switch (config.AttributeErrorHandlingMode)
            {
                case ErrorHandlingMode.LogAndIgnore:
                    logger.LogError(exceptionMessage);
                    return false;
                case ErrorHandlingMode.Throw:
                    throw new AmpException(exceptionMessage);
                default:
                    throw new ArgumentOutOfRangeException($"Unsuported {nameof(config.AttributeErrorHandlingMode)}");
            }

        }

        public bool CheckStyleAttribute(string name, string value)
        {
            return true; //todo
        }

        public bool ValidateKnockoutDataBind(string name)
        {
            var errorMessage = "Control tried to use knockout dataBind, which is unsupported during amp rendering.";

            switch (config.KnockoutErrorHandlingMode)
            {

                case ErrorHandlingMode.Throw:
                    throw new AmpException(errorMessage);
                case ErrorHandlingMode.LogAndIgnore:
                    logger.LogError(errorMessage);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}
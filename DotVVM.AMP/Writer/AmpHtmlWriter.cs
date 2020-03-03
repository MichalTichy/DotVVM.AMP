using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.Framework.Controls;

namespace DotVVM.AMP.Writer
{
    public class AmpHtmlWriter : IHtmlWriter
    {
        private IHtmlWriter writer;
        private readonly DotvvmAmpConfig ampConfig;

        public AmpHtmlWriter(IHtmlWriter writer, DotvvmAmpConfig ampConfig)
        {
            this.writer = writer;
            this.ampConfig = ampConfig;
        }

        public void AddAttribute(string name, string value, bool append = false, string appendSeparator = null)
        {
            writer.AddAttribute(name, value, append, appendSeparator);
        }

        public void AddStyleAttribute(string name, string value)
        {
            writer.AddStyleAttribute(name, value);
        }

        public void AddKnockoutDataBind(string name, string expression)
        {
            if (ampConfig.ErrorHandlingMode == ErrorHandlingMode.Throw)
                throw new DotvvmControlException("Control tried to use knockout dataBind, which is unsupported during amp rendering.");
        }

        public void AddKnockoutDataBind(string name, KnockoutBindingGroup bindingGroup)
        {
            if (ampConfig.ErrorHandlingMode == ErrorHandlingMode.Throw)
                throw new DotvvmControlException("Control tried to use knockout dataBind, which is unsupported during amp rendering.");
        }

        public void RenderBeginTag(string name)
        {
            writer.RenderBeginTag(name);
        }

        public void RenderSelfClosingTag(string name)
        {
            writer.RenderSelfClosingTag(name);
        }

        public void RenderEndTag()
        {
            writer.RenderEndTag();
        }

        public void WriteText(string text)
        {
            writer.WriteText(text);
        }

        public void WriteUnencodedText(string text)
        {
            writer.WriteUnencodedText(text);
        }

        public void WriteHtmlAttribute(string attributeName, string attributeValue)
        {
            writer.WriteHtmlAttribute(attributeName, attributeValue);
        }

        public void SetErrorContext(DotvvmBindableObject obj)
        {
            writer.SetErrorContext(obj);
        }
    }
}
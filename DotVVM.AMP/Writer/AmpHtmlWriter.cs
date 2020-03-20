using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using DotVVM.AMP.Config;
using DotVVM.AMP.Enums;
using DotVVM.AMP.Validator;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.AMP.Writer
{
    public class AmpHtmlWriter : IAmpHtmlWriter
    {
        protected IHtmlWriter writer;
        protected readonly DotvvmAmpConfiguration AmpConfiguration;
        protected readonly IAmpValidator validator;

        public AmpHtmlWriter(DotvvmAmpConfiguration configuration, StreamWriter textWriter, IDotvvmRequestContext context, IAmpValidator validator)
        {
            writer = new HtmlWriter(textWriter, context);
            this.AmpConfiguration = configuration;
            this.validator = validator;
        }

        public virtual void AddAttribute(string name, string value, bool append = false, string appendSeparator = null)
        {
            if (validator.CheckAttribute(name, value))
            {
                writer.AddAttribute(name, value, append, appendSeparator);
            }
        }

        public virtual void AddStyleAttribute(string name, string value)
        {
            if (validator.CheckStyleAttribute(name, value))
            {
                writer.AddStyleAttribute(name, value);
            }
        }

        public virtual void AddKnockoutDataBind(string name, string expression)
        {
            if (validator.ValidateKnockoutDataBind(name))
                writer.AddKnockoutDataBind(name, expression); // this should never happen because knockout bindings are invalid in AMP
        }

        public virtual void AddKnockoutDataBind(string name, KnockoutBindingGroup bindingGroup)
        {
            if (validator.ValidateKnockoutDataBind(name))
                writer.AddKnockoutDataBind(name, bindingGroup); // this should never happen because knockout bindings are invalid in AMP
        }

        public virtual void RenderBeginTag(string name)
        {
            //tags are not validated, because all invalid tags should be replaced by their amp alternatives by now
            writer.RenderBeginTag(name);
        }

        public virtual void RenderSelfClosingTag(string name)
        {
            //tags are not validated, because all invalid tags should be replaced by their amp alternatives by now
            writer.RenderSelfClosingTag(name);
        }

        public virtual void RenderEndTag()
        {
            //tags are not validated, because all invalid tags should be replaced by their amp alternatives by now
            writer.RenderEndTag();
        }

        public virtual void WriteText(string text)
        {
            writer.WriteText(text);
        }

        public virtual void WriteUnencodedText(string text)
        {
            writer.WriteUnencodedText(text);
        }

        public virtual void WriteHtmlAttribute(string name, string value)
        {
            if (validator.CheckAttribute(name, value))
            {
                writer.WriteHtmlAttribute(name, value);
            }
        }

        public virtual void SetErrorContext(DotvvmBindableObject obj)
        {
            writer.SetErrorContext(obj);
        }
    }
}
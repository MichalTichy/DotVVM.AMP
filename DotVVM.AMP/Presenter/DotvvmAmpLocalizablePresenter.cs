using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.AMP.Presenter
{
    public class DotvvmAmpLocalizablePresenter : LocalizablePresenter, IAmpPresenter
    {
        public static Func<IServiceProvider, IAmpPresenter> BasedOnParameterWithAmp(string name, bool redirectWhenNotFound = true)
        {
            DotvvmAmpLocalizablePresenter presenter = new DotvvmAmpLocalizablePresenter(redirectWhenNotFound ? LocalizablePresenter.WithRedirectOnFailure(new Action<IDotvvmRequestContext>(redirect), new Func<IDotvvmRequestContext, CultureInfo>(getCulture)) : new Func<IDotvvmRequestContext, CultureInfo>(getCulture), (Func<IDotvvmRequestContext, Task>)(context => context.Services.GetRequiredService<IAmpPresenter>().ProcessRequest(context)));
            return _ => presenter;

            void redirect(IDotvvmRequestContext context)
            {
                Dictionary<string, object> dictionary = context.Parameters.ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>)(e => e.Key), (Func<KeyValuePair<string, object>, object>)(e => e.Value));
                if (context.Configuration.DefaultCulture.Equals(dictionary[name]))
                    throw new Exception("The specified default culture is probably invalid");
                dictionary[name] = (object)context.Configuration.DefaultCulture;
                context.RedirectToRoute(context.Route.RouteName, (object)dictionary, false, true, (string)null, (object)context.HttpContext.Request.Query);
            }

            CultureInfo getCulture(IDotvvmRequestContext context)
            {
                object obj;
                return !context.Parameters.TryGetValue(name, out obj) || string.IsNullOrEmpty(obj as string) ? (CultureInfo)null : CultureInfo.GetCultureInfo((string)obj);
            }
        }

        public DotvvmAmpLocalizablePresenter(Func<IDotvvmRequestContext, CultureInfo?> getCulture, Func<IDotvvmRequestContext, Task> nextPresenter) : base(getCulture, nextPresenter)
        {
        }
    }
}
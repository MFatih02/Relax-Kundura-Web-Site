using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RelaxKundura3.Extentions
{
    [HtmlTargetElement("custom-button", Attributes = "asp-controller, asp-action, icon-class")]
    public class CustomButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("icon-class")]
        public string IconClass { get; set; }

        private readonly IUrlHelperFactory _urlHelperFactory;

        public CustomButtonTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var url = urlHelper.Action(Action, Controller);

            output.TagName = "a";
            output.Attributes.SetAttribute("href", url);
            output.Attributes.SetAttribute("class", "btn btn-primary"); 

            output.Content.Append("Yeni Ekle ");
            output.Content.AppendHtml($"<i class='{IconClass}'></i>");
        }
    }
}

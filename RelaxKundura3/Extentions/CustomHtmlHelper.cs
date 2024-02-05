using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq.Expressions;

public static class MyHtmlExtensions
{
    public static IHtmlContent CinsiyetDropListFor(this IHtmlHelper helper, string expression, object htmlAttributes)
    {
        var labelTagBuilder = new TagBuilder("label");
        labelTagBuilder.AddCssClass("col-md-4 form-label");
        labelTagBuilder.Attributes.Add("for", expression);
        labelTagBuilder.InnerHtml.Append("Cinsiyet");

        var mainDiv = new TagBuilder("div");
        mainDiv.AddCssClass("form-group");
        mainDiv.InnerHtml.AppendHtml(labelTagBuilder);

        var colDivTagBuilder = new TagBuilder("div");
        colDivTagBuilder.AddCssClass("col-md-8");

        var selectList = new SelectListItem[]
        {
            new SelectListItem { Text = "Erkek", Value = "Erkek" },
            new SelectListItem { Text = "Kadın", Value = "Kadın" }
        };

        // Bu satırı ekleyerek "Seçiniz" değeri ile başlamasını sağlayabilirsiniz.
        selectList = selectList.Prepend(new SelectListItem { Text = "Seçiniz", Value = "" }).ToArray();

        var dropDownTag = helper.DropDownList(expression, selectList, htmlAttributes);

        colDivTagBuilder.InnerHtml.AppendHtml(dropDownTag);

        mainDiv.InnerHtml.AppendHtml(colDivTagBuilder);

        return mainDiv;
    }
}

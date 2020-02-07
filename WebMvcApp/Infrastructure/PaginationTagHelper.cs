using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.WebMvcApp.ViewModels;

namespace SportsStore.WebMvcApp.Infrastructure
{
    [HtmlTargetElement("ul", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public string PageItemClass { get; set; }
        public string PageItemClassActive { get; set; }
        public string PageLinkClass { get; set; }

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            var listTagBuilder = new TagBuilder("ul");

            for (var p = 1; p <= PageModel.TotalPages; p++)
            {
                PageUrlValues["page"] = p;

                var listItemTagBuilder = new TagBuilder("li");

                if (p == PageModel.CurrentPage)
                {
                    listItemTagBuilder.AddCssClass(PageItemClassActive);
                }

                listItemTagBuilder.AddCssClass(PageItemClass);

                var anchorTagBuilder = new TagBuilder("a");

                anchorTagBuilder.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

                anchorTagBuilder.AddCssClass(PageLinkClass);

                anchorTagBuilder.InnerHtml.Append(p.ToString());

                listItemTagBuilder.InnerHtml.AppendHtml(anchorTagBuilder);

                listTagBuilder.InnerHtml.AppendHtml(listItemTagBuilder);
            }

            output.Content.AppendHtml(listTagBuilder.InnerHtml);
        }
    }
}

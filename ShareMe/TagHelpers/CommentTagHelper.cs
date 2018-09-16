using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace ShareMe.TagHelpers
{
	[HtmlTargetElement("comment")]
	public class CommentTagHelper : TagHelper
	{
		public string Commenter { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "p";
			var content = await output.GetChildContentAsync();
			output.Content.SetHtmlContent($"{content.GetContent()}, <a href=\"/{Commenter}\">{Commenter}</a>");
		}
	}
}

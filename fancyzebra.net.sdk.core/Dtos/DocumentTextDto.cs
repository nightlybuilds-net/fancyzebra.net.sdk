using System.Collections.Generic;
using System.Linq;

namespace fancyzebra.net.sdk.core.Dtos
{
    public class DocumentTextDto
    {
        public string DocumentId { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string HtmlText { get; set; }
        public string Lang { get; set; }
        public bool IsDefault { get; set; }
    }
}
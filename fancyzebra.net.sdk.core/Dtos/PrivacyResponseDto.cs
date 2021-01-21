using System.Collections.Generic;
using System.Linq;

namespace fancyzebra.net.sdk.core.Dtos
{
    public class PrivacyResponseDto
    {
        public IList<DocumentDto> Documents { get; set; }

        public bool IsAccepted()
        {
            return this.Documents
                .SelectMany(s => s.Clauses)
                .Where(cl => cl.IsMandatory)
                .All(cl => cl.IsAccepted);
        }
    }
}
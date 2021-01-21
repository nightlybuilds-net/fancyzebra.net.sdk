using System.Collections.Generic;

namespace fancyzebra.net.sdk.core.Dtos
{
    public class DocumentDto
    {
        public string Text { get; set; }
        public IList<ClauseDto> Clauses { get; set; }
    }
}
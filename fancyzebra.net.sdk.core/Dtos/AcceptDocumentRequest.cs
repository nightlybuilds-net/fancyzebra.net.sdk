using System.Collections.Generic;

namespace fancyzebra.net.sdk.core.Dtos
{
    public class AcceptDocumentRequest
    {
        public string AppUserId { get; set; }
        public string AppId { get; set; }
        public IEnumerable<AcceptDocumentTextRequest> AcceptedTexts { get; set; }
    }

    public class AcceptDocumentTextRequest
    {
        public string DocumentTextId { get; set; }
        public IEnumerable<AcceptClauseRequest> Clauses { get; set; }
    }

    public class AcceptClauseRequest
    {
        public string ClauseId { get; set; }
        public bool Accepted { get; set; }
    }
}
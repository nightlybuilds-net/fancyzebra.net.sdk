namespace fancyzebra.net.sdk.core.Dtos
{
    public class DocumentToAcceptDto
    {
        public DocumentDto Document { get; set; }
        public DocumentTextDto DocumentText { get; set; }
        public ClauseDto[] Clauses { get; set; }
    }
}
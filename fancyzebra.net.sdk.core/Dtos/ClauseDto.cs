namespace fancyzebra.net.sdk.core.Dtos
{
    public class ClauseDto
    {
        public string Id { get; set; }
        public string DocumentTextId { get; set; }
        public string Text { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsAccepted { get; set; }
    }
}
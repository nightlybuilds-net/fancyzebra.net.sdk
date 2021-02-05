using System.Collections.Generic;

namespace fancyzebra.net.sdk.core.Dtos
{
    public class DocumentDto
    {
        public string DocumentGroup { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public bool IsDraft { get; set; }
        
        /// <summary>
        /// If true language is defined 4 char: it-IT
        /// </summary>
        public bool Use4CharLang { get; set; }

        public string Id { get; set; }
        public string AppId { get; set; }

    }
}
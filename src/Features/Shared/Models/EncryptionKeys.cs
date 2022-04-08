using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Features.Shared.Models
{
    public class EncryptionKeys
    {
        public string AuthorizationEncryptionKey { get; set; } = string.Empty;
        public string DocumentSuffixEncryptionKey { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
    }
}
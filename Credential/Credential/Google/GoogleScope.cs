using System;

namespace Credential.Google
{
    public class GoogleScope
    {
        public Boolean Profile { get; set; }
        public Boolean Email { get; set; }
        public Boolean OpenId { get; set; }
        public Boolean Tasks { get; set; }
        public Boolean TasksReadOnly { get; set; }
    }
}
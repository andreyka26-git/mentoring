using System;

namespace LibraryNetwork.Models
{
    public class Patent: LibraryEntity
    {
        public string Creator { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime TimeExpiration { get; set; }
        public string RegisterNumber { get; set; }

        public override DateTime GetExpiration()
        {
            return TimeExpiration;
        }

        public override string EntityToString()
        {
            var stringRepresentation = base.EntityToString() + '\n';
            stringRepresentation += $"Creator {Creator}, register number: {RegisterNumber} \n";
            stringRepresentation += $"Time received: {TimeReceived}, time expiration: {TimeExpiration}";
            return stringRepresentation;
        }
    }
}

using System;

namespace ProgFrog.Core.Model
{
    public class GuidIdentifier : IIdentifier
    {
        public Guid Id { get; private set; }

        public GuidIdentifier(Guid id)
        {
            this.Id = id;
        }

        public GuidIdentifier(string strGuid)
        {
            Guid id;
            if(Guid.TryParse(strGuid, out id))
            {
                Id = id;
            }
            else
            {
                throw new ArgumentException("Incorrect id format");
            }
        }

        public string StringPresentation
        {
            get
            {
                return Id.ToString();
            }
        }
    }
}

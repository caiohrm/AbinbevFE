using System;
namespace CrossCutting.Models
{
	public class BaseEntity 
	{
		public BaseEntity()
		{
            CreatedDate = DateTime.UtcNow;
		}

		public int Id { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public void Disable()
        {
            Enabled = false;
            UpdatedDate = DateTime.UtcNow;
        }

        public void Enable()
        {
            Enabled = true;
            UpdatedDate = DateTime.UtcNow;
        }

        public int CompareTo(BaseEntity? other)
        {
            if (other == null)
            {
                return 1;
            }

            return other!.Id.CompareTo(Id);
        }
    }
}


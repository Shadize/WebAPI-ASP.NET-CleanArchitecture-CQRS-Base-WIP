using WebAPI.Domain.Common;

namespace WebAPI.Domain.Entities
{
    public class Example : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        private Example() { }
        public Example(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

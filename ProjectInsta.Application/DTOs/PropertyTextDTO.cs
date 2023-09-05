using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.DTOs
{
    public class PropertyTextDTO
    {
        public int Id { get; set; }
        public double? Top { get; set; }
        public double? Left { get; set; }
        public string? Text { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? Background { get; set; }
        public string? FontFamily { get; set; }

        public int? StoryId { get; set; }

        //public int? StoryIdRefTable { get; set; }
        public StoryDTO? Story { get; set; }
    }
}

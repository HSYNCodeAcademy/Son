using Microsoft.AspNetCore.Http;

namespace EduhomePraktika._1.ViewModels
{
    public class SliderCreate
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageURL { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

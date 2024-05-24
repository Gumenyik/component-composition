using System.ComponentModel.DataAnnotations;

namespace component_composition.ViewModels
{
    public class NewAggregateModel
    {
        [Required(ErrorMessage = "Не вказано іи'я!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не вказанний опис!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Виберіть зображення!")]
        [FileExtensions(Extensions = "jpg,jpeg,png,gif,bmp,tiff")]
        public byte[] Image { get; set; }
    }
}

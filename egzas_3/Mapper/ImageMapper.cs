﻿//using egzas_3.Mapper.Interfaces;
//using TodoApp.API.Dtos.Requests;
//using TodoApp.API.Dtos.Results;
//using TodoApp.DAL.Entities;

//namespace egzas_3.Mapper
//{
//    public class ImageMapper : IImageMapper
//    {
//        public ImageResultDto Map(Image entity)
//        {
//            return new ImageResultDto
//            {
//                Id = entity.Id,
//                Name = entity.Name,
//                Description = entity.Description,
//            };
//        }

//        public List<ImageResultDto> Map(IEnumerable<Image> entities)
//        {
//            return entities.Select(Map).ToList();
//        }


//        public Image Map(ImageUploadRequestDto dto, long todoItemId)
//        {
//            using var stream = new MemoryStream();
//            dto.Image.CopyTo(stream);
//            var imageBytes = stream.ToArray();
//            return new Image
//            {
//                Name = dto.Name!,
//                Description = dto.Description!,
//                TodoItemId = todoItemId,
//                ImageBytes = imageBytes,
//            };
//        }
//    }
//}
using AutoMapper;
using Notes.Application.Common.Mapping;
using Notes.Application.Notes.Commands.UpdateNote;
using System;

namespace Notes.WebApi.Models
{
    public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(updateCommand => updateCommand.Id, opt => opt.MapFrom(dto => dto.Id))
                .ForMember(updateCommand => updateCommand.Title, opt => opt.MapFrom(dto => dto.Title))
                .ForMember(updateCommand => updateCommand.Details, opt => opt.MapFrom(dto => dto.Details));
        }
    }
}

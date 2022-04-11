using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers
{
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId,
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserId,
                Id = id
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody]CreateNoteDto createNoteDto)
        {
            var createNoteCommand = _mapper.Map<CreateNoteCommand>(createNoteDto);
            createNoteCommand.UserId = UserId;

            Guid noteId = await Mediator.Send(createNoteCommand);

            return Ok(noteId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateNoteDto updateNoteDto)
        {
            var updateCommand = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            updateCommand.UserId = UserId;

            await Mediator.Send(updateCommand);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                UserId = UserId,
                Id = id
            };

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
